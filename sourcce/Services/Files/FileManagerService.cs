using IRepositories;
using IServices.Files;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.Files
{
    public class FileManagerService : IFileManagerService
    {

        readonly ISharePointProvider _sharepointConnectionProvider;

        public FileManagerService(ISharePointProvider pSharepointConnectionProvider)
        {
            _sharepointConnectionProvider = pSharepointConnectionProvider;
        }


        public string DeleteAllFilesByProduct(Guid pLeadID, short pCompanyID, short pCampaignID, short pProductID)
        {
            string result = "";

            try
            {
                using (ClientContext clientContext = new ClientContext(_sharepointConnectionProvider.GetSharePointUrl()))
                {
                    clientContext.Credentials = GetCredentials();
                    clientContext.Load(clientContext.Web);
                    clientContext.ExecuteQuery();

                    var list = clientContext.Web.Lists.GetByTitle("Documents");
                    var folderName = _sharepointConnectionProvider.GetSharePointFolder();

                    clientContext.Load(list.RootFolder);
                    clientContext.ExecuteQuery();


                    //Eliminar imagen
                    var fileDeleteUrl = String.Format("{0}/{1}/{2}", list.RootFolder.ServerRelativeUrl, folderName, pLeadID.ToString());

                    Folder companyFolder = clientContext.Web.GetFolderByServerRelativeUrl(fileDeleteUrl);
                    clientContext.Load(companyFolder);
                    clientContext.Load(companyFolder.Files);
                    clientContext.Load(companyFolder.Folders);
                    clientContext.ExecuteQuery();
                    
                    if (companyFolder.Files != null && companyFolder.Files.Count() > 0)
                    {
                        string _substring = string.Format("{0}_{1}_{2}", pCompanyID.ToString(), pCampaignID.ToString(), pProductID.ToString());
                        IEnumerable<File> companyFiles = companyFolder.Files.Where(x => x.Name.Contains(_substring) && x.Name.IndexOf(_substring) == 0);

                        companyFiles.ToList().ForEach(file => file.DeleteObject());
                        clientContext.ExecuteQuery();
                    }

                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }

        public string DeleteProductFile(Guid pLeadID, short pCompanyID, short pCampaignID, short pProductID, short pFileNumber)
        {
            string result = "";

            try
            {
                using (ClientContext clientContext = new ClientContext(_sharepointConnectionProvider.GetSharePointUrl()))
                {
                    clientContext.Credentials = GetCredentials();
                    clientContext.Load(clientContext.Web);
                    clientContext.ExecuteQuery();

                    var list = clientContext.Web.Lists.GetByTitle("Documents");
                    var folderName = _sharepointConnectionProvider.GetSharePointFolder();

                    clientContext.Load(list.RootFolder);
                    clientContext.ExecuteQuery();


                    string companyFolderUrl = String.Format("{0}/{1}/{2}", list.RootFolder.ServerRelativeUrl, folderName, pLeadID.ToString());

                    Folder companyFolder = clientContext.Web.GetFolderByServerRelativeUrl(companyFolderUrl);
                    clientContext.Load(companyFolder);
                    clientContext.Load(companyFolder.Files);
                    clientContext.Load(companyFolder.Folders);
                    clientContext.ExecuteQuery();

                    if (companyFolder.Files != null && companyFolder.Files.Count() > 0)
                    {
                        string fileName = string.Format("{0}_{1}_{2}_{3}", pCompanyID.ToString(), pCampaignID.ToString(), pProductID.ToString(), pFileNumber.ToString());
                        File fileCompany = companyFolder.Files.Where(x => x.Name.Substring(0, x.Name.LastIndexOf(".")).Equals(fileName)).FirstOrDefault();

                        if (fileCompany != null)
                        {
                            fileCompany.DeleteObject();
                            clientContext.ExecuteQuery();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }

        public string SaveFile(HttpPostedFileBase file, Guid pLeadID, short pCompanyID, short pCampaignID, short pProductID, short pFileNumber)
        {
            string result = "";

            try
            {
                using (ClientContext clientContext = new ClientContext(_sharepointConnectionProvider.GetSharePointUrl()))
                {
                    clientContext.Credentials = GetCredentials();
                    clientContext.Load(clientContext.Web);
                    clientContext.ExecuteQuery();

                    var list = clientContext.Web.Lists.GetByTitle("Documents");

                    clientContext.Load(list.RootFolder);
                    clientContext.ExecuteQuery();


                    bool folderCreated = CheckCompanyFolder(clientContext, pLeadID.ToString());

                    string folderName = _sharepointConnectionProvider.GetSharePointFolder();

                    string ext = file.ContentType.Substring(file.ContentType.LastIndexOf("/") + 1).ToLower();
                    string actualFileName = pCompanyID.ToString() + "_" + pCampaignID.ToString() + "_" + pProductID.ToString() + "_" + pFileNumber.ToString() + "." + ext;


                    var fileUrl = String.Format("{0}/{1}/{2}/{3}", list.RootFolder.ServerRelativeUrl, folderName, pLeadID.ToString(), actualFileName);
                    File.SaveBinaryDirect(clientContext, fileUrl, file.InputStream, true);
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }


        public SharePointOnlineCredentials GetCredentials()
        {
            string userName = _sharepointConnectionProvider.GetSharePointUser();
            string password = _sharepointConnectionProvider.GetSharePointPassword();
            var securePassword = new SecureString();

            foreach (var c in password)
            {
                securePassword.AppendChar(c);
            }

            return new SharePointOnlineCredentials(userName, securePassword);
        }

        public bool CheckCompanyFolder(ClientContext pClientContext, string pLeadId)
        {
            bool result = false;

            string folderName = _sharepointConnectionProvider.GetSharePointFolder();
            pClientContext.Credentials = GetCredentials();
            pClientContext.Load(pClientContext.Web);
            pClientContext.ExecuteQuery();

            var list = pClientContext.Web.Lists.GetByTitle("Documents");

            pClientContext.Load(list.RootFolder);
            pClientContext.ExecuteQuery();


            //Crear el folder principal
            try
            {
                ListItemCreationInformation newItemInfo = new ListItemCreationInformation();
                newItemInfo.UnderlyingObjectType = FileSystemObjectType.Folder;
                newItemInfo.LeafName = folderName;
                ListItem newListItem = list.AddItem(newItemInfo);
                newListItem["Title"] = folderName;
                newListItem.Update();
                pClientContext.ExecuteQuery();
            }
            catch (Exception e)
            {
                //Si entra aqui es porque ya existia una carpeta con ese nombre
            }
            
            //Crear el folder ppara la compañia
            try
            {
                var companyFolderUrl = String.Format("{0}/{1}", list.RootFolder.ServerRelativeUrl, folderName);
                Folder folder = pClientContext.Web.GetFolderByServerRelativeUrl(companyFolderUrl);

                pClientContext.Load(folder);
                pClientContext.Load(folder.Folders);
                pClientContext.ExecuteQuery();
                //it runs till next line
                Folder companyFolder = folder.Folders.Where(x => x.Name.Equals(pLeadId)).FirstOrDefault();

                if (companyFolder== null)
                {
                    folder.Folders.Add(pLeadId);
                    pClientContext.ExecuteQuery();
                }
                pClientContext.Load(folder);
                pClientContext.Load(folder.Folders);
                pClientContext.ExecuteQuery();
                result = folder.Folders.Where(x => x.Name.Equals(pLeadId)).FirstOrDefault() == null ? false : true;

            }
            catch (Exception e)
            {
                //Si entra aqui es porque ya existia una carpeta con ese nombre
            }

            return result;
        }

    }
}
