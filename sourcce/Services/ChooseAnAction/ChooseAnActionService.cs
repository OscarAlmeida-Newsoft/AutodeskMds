using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IServices;
using DTOs;
using System.Configuration;
using IRepositories;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Entities;
using System.Web;
using System.Xml;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Services
{
    public class ChooseAnActionService : IChooseAnActionService
    {

        public string Message { get; set; }
        readonly ILeadRepository leadRespository;
        readonly IAssessmentSummaryRepository assessmentSummaryRepository;
        readonly IAssessmentService assessmentService;

        readonly ICompanyRepository companyRespository;
        readonly ICompanyInfoRepository companyInfoRespository;
        readonly ICompanyContactsRepository companyContactsRespository;

        readonly ISAM360Provider SAM360Provider;

        readonly ILeadService leadService;
        readonly IMDSService MDSService;

        public ChooseAnActionService(ILeadRepository pLeadRepository, IAssessmentSummaryRepository pAssessmentSummaryRepository, IAssessmentService pAssessmentService,
                     ICompanyRepository pCompanyRespository, ICompanyInfoRepository pCompanyInfoRespository, ICompanyContactsRepository pCompanyContactsRespository,
                     ISAM360Provider pSAM360Provider, ILeadService pLeadService, IMDSService pMDSService)
        {
            leadRespository = pLeadRepository;
            assessmentSummaryRepository = pAssessmentSummaryRepository;
            assessmentService = pAssessmentService;

            companyRespository = pCompanyRespository;
            companyInfoRespository = pCompanyInfoRespository;
            companyContactsRespository = pCompanyContactsRespository;
            SAM360Provider = pSAM360Provider;
            leadService = pLeadService;
            MDSService = pMDSService;
        }

        public string GetUrlRedirectMeasureMyPlataform(LeadInformationDTO user)
        {
            MeasureMyPlataformServices.MeasureMyPlataformServicesClient _measureMyPlataformService = new MeasureMyPlataformServices.MeasureMyPlataformServicesClient();

            MeasureMyPlataformServices.UserSAMLive userSAMLive = new MeasureMyPlataformServices.UserSAMLive();
            userSAMLive.username = user.CompanySAMLiveUserName;

            userSAMLive.Company = user.CompanyName;
            userSAMLive.Email = user.Email;
            userSAMLive.FirstName = user.FirstName;
            userSAMLive.LastName = user.LastName;
            userSAMLive.Telephone = user.Telephone;
            userSAMLive.Mobil = user.MobilePhone;
            userSAMLive.RefUserGroupID = ConfigurationManager.AppSettings["SAMLive_SmallPlan_GroupID"];
            userSAMLive.DefaultDashboard = ConfigurationManager.AppSettings["SAMLive_DefaultDashboardId"];

            MeasureMyPlataformServices.TokenResponse response = _measureMyPlataformService.GetUrlRedirectMeasureMyPlataform(userSAMLive);

            if (response.error)
            {
                Message = response.message;
            }

            return response.Url;

        }


        public ChooseAnActionSummaryDTO GetLeadGeneralProgress(Guid pCompanyId, Guid pCampaignId)
        {
            ChooseAnActionSummaryDTO _data = new ChooseAnActionSummaryDTO();
            _data.MDSProgressValue = leadRespository.GetLeadInformationProgress(pCompanyId);
            _data.AvailableAssessments = assessmentService.GetActiveAssessmentsCount();
            _data.FinishedAssessments = assessmentSummaryRepository.GetFinishedAssessmentCount(pCompanyId, pCampaignId);

            if (_data.AvailableAssessments > 0)
            {
                double _div = (_data.FinishedAssessments / Convert.ToDouble(_data.AvailableAssessments)) * 100;
                _data.AssessmentsProgress = (int)Math.Ceiling(_div);
            }
            else
            {
                _data.AssessmentsProgress = 0;
            }

            return _data;
        }

        public bool GetLeadMDSStatus(Guid pCompanyId)
        {
            return leadRespository.GetLeadInformationProgress(pCompanyId) == 100;
        }

        public double GetLeadMDSPercentage(Guid pLeadId, short pCampaignID)
        {
            double result = 0;

            var _currentCompanyInfo = companyInfoRespository.GetCompanyIDByLeadAndCampaign(pLeadId, pCampaignID);

            if (_currentCompanyInfo != null)
            {
                if (_currentCompanyInfo.IsFinalVersion)
                {
                    result = 100;
                }
                //Si no tiene una version final entonces se hace el conteo
                else
                {
                    var _currentCompany = companyRespository.GetCompanyByID(_currentCompanyInfo.CompanyID);
                    var _currentCompanyContact = companyContactsRespository.GetByIDandCampaign(_currentCompanyInfo.CompanyID, _currentCompanyInfo.CampaignID);

                    if (_currentCompany != null && _currentCompanyContact != null)
                    {
                        double sum = 0;
                        double totalFields = 0;

                        //########################################################### COMPANY CONTACTS ###########################################################
                        if (_currentCompanyContact.ContactName != null && _currentCompanyContact.ContactName.Length > 0)
                        {
                            sum++;
                        }
                        totalFields++;

                        if (_currentCompanyContact.CompanyEmail != null && _currentCompanyContact.CompanyEmail.Length > 0)
                        {
                            sum++;
                        }
                        totalFields++;

                        if (_currentCompanyContact.CompanyPhone != null && _currentCompanyContact.CompanyPhone.Length > 0)
                        {
                            sum++;
                        }
                        totalFields++;

                        //########################################################### COMPANY ###########################################################


                        if (_currentCompany.IndustryID != null) //SI o no?
                        {
                            sum++;
                        }
                        totalFields++;


                        if (_currentCompany.CompanyTypeID != null) //SI o no?
                        {
                            sum++;
                        }
                        totalFields++;

                        //########################################################### COMPANY INFO ###########################################################

                        if (_currentCompanyInfo.TotalQuantityOfEmployees != null) //SI o no?
                        {
                            sum++;
                        }
                        totalFields++;

                        if (_currentCompanyInfo.TotalQuantityOfDesktops != null) //SI o no?
                        {
                            sum++;
                        }
                        totalFields++;

                        if (_currentCompanyInfo.SoftwareAssuranceFlag != null) //SI o no?
                        {
                            sum++;
                        }
                        totalFields++;

                        if (_currentCompanyInfo.PlansToPurchaseNewLicensesFlag != null) //SI o no?
                        {
                            sum++;
                        }
                        totalFields++;

                        if (_currentCompanyInfo.PlansToUpgradeCurrentlyOwnedLicensesFlag != null) //SI o no?
                        {
                            sum++;
                        }
                        totalFields++;

                        if (_currentCompanyInfo.AuthorizedMicrosoftChannelFlag != null) //SI o no?
                        {
                            sum++;
                        }
                        totalFields++;

                        if (_currentCompanyInfo.LicenseStatusResponseID != null) //SI o no?
                        {
                            sum++;
                        }
                        totalFields++;


                        result = Math.Round((sum / (totalFields + 1)) * 100, 0);

                    }
                }
            }
            //Si no existe la compañia en CompanyInfo quiere decir que aun no ha abierto mds
            else
            {
                result = 0;
            }

            return result;
        }




        #region SAM360 logic
        public string GetUrlRedirectMeasureMyPlataformSAM360(LeadInformationDTO user, CRMDataDTO crmUser)
        {
            string redirectLink = "";
            //Si entra aqui quiere decir que el usuario de SAM360 ya existe 
            if (user.SAM360OrganisationId != null && user.SAM360CompanyUser != null && user.SAM360CompanyPassword != null)
            {
                redirectLink = GetEncodedSAM360Url(user.SAM360CompanyUser, user.SAM360CompanyPassword);
            }
            //Si entra aqui quiere decir que hay que crear la organizacion y el usuario en SAM360
            else
            {
                try
                {
                    LeadInformation _leadInfo = leadRespository.GetByLeadId(user.LeadId);


                    string _NumberOfEmp = crmUser.NumberOfEmployees.ToString();
                    string _NumberOfPCs = crmUser.NumberOfPCs.ToString();
                    var _passwordTemplate = "#" + _NumberOfEmp + "$" + _NumberOfPCs + "*";


                    if (user.SAM360OrganisationId == null)
                    {
                        string SAM360CompanyName = user.CompanyName;
                        int SAM360CompanyRegisteredDevices = Int32.Parse(_NumberOfPCs);

                        int SAM360OrganisationId = CreateSAM360Organisation(SAM360CompanyName, SAM360CompanyRegisteredDevices);

                        if (SAM360OrganisationId != -1)
                        {
                            _leadInfo.SAM360OrganisationId = SAM360OrganisationId;
                            MDSService.CommitChangesAffidavit();

                        }
                        else
                        {
                            //¿Que pasa si viene en -1?
                            throw new Exception();
                        }
                    }

                    if (user.SAM360CompanyUser == null || user.SAM360CompanyPassword == null)
                    {
                        string SAM360CompanyUser = user.LeadUser;

                        if (SAM360CompanyUser.Equals("julian.giraldo@e-chez.com"))
                        {
                            string date = DateTime.Now.ToString("ddmmyyyyHHmmss");
                            SAM360CompanyUser = SAM360CompanyUser.Replace("@", date + "@");
                        }

                        string SAM360CompanyPassword = _passwordTemplate;

                        int SAM360UserId = CreateSAM360User(_leadInfo.SAM360OrganisationId.Value, SAM360CompanyUser, SAM360CompanyPassword);

                        if (SAM360UserId != -1)
                        {
                            _leadInfo.SAM360UserId = SAM360UserId;
                            _leadInfo.SAM360CompanyUser = SAM360CompanyUser;
                            _leadInfo.SAM360CompanyPassword = SAM360CompanyPassword;

                            MDSService.CommitChangesAffidavit();
                        }
                        else
                        {
                            //¿Que pasa si viene en -1?
                            throw new Exception();
                        }
                    }

                    redirectLink = GetEncodedSAM360Url(_leadInfo.SAM360CompanyUser, _leadInfo.SAM360CompanyPassword);
                }
                catch (Exception e)
                {
                    redirectLink = "";
                }
            }

            return redirectLink;
        }

        public string GetUrlManagmentPoint_SAM360(LeadInformationDTO user, CRMDataDTO crmUser)
        {
            string urlManagmentPoint = "";

            //Si entra aqui quiere decir que el usuario de SAM360 ya existe 
            if (user.SAM360OrganisationId != null && user.SAM360CompanyUser != null && user.SAM360CompanyPassword != null)
            {
                urlManagmentPoint = GetParameter_AllActiveClientOrganisationsGeneralDetails(user.SAM360OrganisationId.ToString(), "Organisation-ManagementPoint");
            }//Si entra aqui quiere decir que hay que crear la organizacion y el usuario en SAM360
            else
            {
                try
                {
                    LeadInformation _leadInfo = leadRespository.GetByLeadId(user.LeadId);


                    string _NumberOfEmp = crmUser.NumberOfEmployees.ToString();
                    string _NumberOfPCs = crmUser.NumberOfPCs.ToString();
                    var _passwordTemplate = "#" + _NumberOfEmp + "$" + _NumberOfPCs + "*";


                    if (user.SAM360OrganisationId == null)
                    {
                        string SAM360CompanyName = user.CompanyName;
                        int SAM360CompanyRegisteredDevices = Int32.Parse(_NumberOfPCs);

                        int SAM360OrganisationId = CreateSAM360Organisation(SAM360CompanyName, SAM360CompanyRegisteredDevices);

                        if (SAM360OrganisationId != -1)
                        {
                            _leadInfo.SAM360OrganisationId = SAM360OrganisationId;
                            MDSService.CommitChangesAffidavit();

                        }
                        else
                        {
                            //¿Que pasa si viene en -1?
                            throw new Exception();
                        }
                    }

                    if (user.SAM360CompanyUser == null || user.SAM360CompanyPassword == null)
                    {
                        string SAM360CompanyUser = user.LeadUser;

                        if (SAM360CompanyUser.Equals("julian.giraldo@e-chez.com"))
                        {
                            string date = DateTime.Now.ToString("ddmmyyyyHHmmss");
                            SAM360CompanyUser = SAM360CompanyUser.Replace("@", date + "@");
                        }

                        string SAM360CompanyPassword = _passwordTemplate;

                        int SAM360UserId = CreateSAM360User(_leadInfo.SAM360OrganisationId.Value, SAM360CompanyUser, SAM360CompanyPassword);

                        if (SAM360UserId != -1)
                        {
                            _leadInfo.SAM360UserId = SAM360UserId;
                            _leadInfo.SAM360CompanyUser = SAM360CompanyUser;
                            _leadInfo.SAM360CompanyPassword = SAM360CompanyPassword;

                            MDSService.CommitChangesAffidavit();
                        }
                        else
                        {
                            //¿Que pasa si viene en -1?
                            throw new Exception();
                        }
                    }

                    urlManagmentPoint = GetParameter_AllActiveClientOrganisationsGeneralDetails(_leadInfo.SAM360OrganisationId.Value.ToString(), "Organisation-ManagementPoint");
                }
                catch (Exception e)
                {
                    urlManagmentPoint = "";
                }
            }

            return urlManagmentPoint;
        }

        public string GetParameter_AllActiveClientOrganisationsGeneralDetails(string companyId, string parameter) {
            string value = "";

            var client = new HttpClient { BaseAddress = new Uri(SAM360Provider.GetSAM360ApiUrl()) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetSAM360Token());

            var response = client.GetAsync("api/Report/GetRecords?reportId=OrganisationsAllActiveClientOrganisationsGeneralDetails").Result;
            var responseData = response.Content.ReadAsStringAsync().Result.Replace(" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\"", "");

            XDocument otraForma = XDocument.Parse(responseData);
            var rows = otraForma.Root.Descendants("ArrayOfstring");

            bool isFirstElement = true;
            int idParameter = 0;
            int cont = 0;
            foreach (var aux in rows)
            {
                List<string> auxList = aux.Elements("string").Select(element => element.Value).ToList();

                if (isFirstElement)
                {

                    //Busco el Id de parametro
                    foreach (string elemento in auxList)
                    {
                        if (elemento == parameter)
                        {
                            idParameter = cont;
                            break;
                        }
                        else
                        {
                            cont++;
                        }
                    }

                    if (idParameter == 0)
                    {
                        break;
                    }
                    else {
                        isFirstElement = false;
                    }
                }
                else {
                    //Busco el valor
                    if (auxList[23] == companyId) {
                        value = auxList[idParameter];
                        break;
                    }
                }

                
            }

            return value;
        }

        

        public List<List<string>> GetSAM360Reports(int pOrganisationId, string pReportId)
        {
            List<List<string>> reportList = new List<List<string>>();

            var client = new HttpClient { BaseAddress = new Uri(SAM360Provider.GetSAM360ApiUrl()) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetSAM360Token());

            var response = client.GetAsync(string.Format("/api/Report/GetRecords?organisationId={0}&reportId={1}",pOrganisationId,pReportId)).Result;            
            var responseData = response.Content.ReadAsStringAsync().Result.Replace(" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\"", "");

            XDocument otraForma = XDocument.Parse(responseData);
            var rows = otraForma.Root.Descendants("ArrayOfstring");

            foreach(var aux in rows)
            {
                List<string> auxList = aux.Elements("string").Select(element => element.Value).ToList();
                reportList.Add(auxList);
            }          

            return reportList;
        }




        private int CreateSAM360Organisation(string pOrganisationName, int pRegisteredDevices)
        {
            int result = -1;

            var data = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Name", pOrganisationName),
                new KeyValuePair<string, string>("RegisteredDevices", pRegisteredDevices.ToString())
            };

            var content = new FormUrlEncodedContent(data);
            var client = new HttpClient { BaseAddress = new Uri(SAM360Provider.GetSAM360ApiUrl()) };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetSAM360Token());
            var response = client.PostAsync("/api/Organisations", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var contents = response.Content.ReadAsStringAsync().Result;
                //TODO: Guardar id de la compañia creada
                result = Int32.Parse(contents);
            }

            return result;
        }

        private int CreateSAM360User(int pOrganisationId, string pEmailAddress, string pPassword)
        {
            int result = -1;

            var data = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("OrganisationId", pOrganisationId.ToString()),
                new KeyValuePair<string, string>("EmailAddress", pEmailAddress),
                new KeyValuePair<string, string>("Password", pPassword),
                new KeyValuePair<string, string>("Role", "Administrator")
            };

            var content = new FormUrlEncodedContent(data);

            var client = new HttpClient { BaseAddress = new Uri(SAM360Provider.GetSAM360ApiUrl()) };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetSAM360Token());

            // call sync
            var response = client.PostAsync("/api/Users", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var contents = response.Content.ReadAsStringAsync().Result;
                result = Int32.Parse(contents);
            }
            return result;
        }

        private string GetSAM360Token()
        {
            string token = "";

            var data = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", SAM360Provider.GetSAM360AdminUser()),
                    new KeyValuePair<string, string>("password", SAM360Provider.GetSAM360AdminPassword())
                };

            var content = new FormUrlEncodedContent(data);

            var client = new HttpClient { BaseAddress = new Uri(SAM360Provider.GetSAM360ApiUrl()) };

            // call sync
            var response = client.PostAsync("/token", content).Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic jobject = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                token = jobject.access_token;
            }

            return token;
        }

        private string GetEncodedSAM360Url(string pSAM360CompanyUser, string pSAM360CompanyPassword)
        {
            string encodedUserUrl = HttpUtility.UrlEncode(SAM360XOR(SAM360Provider.GetSAM360UserSharedKey(), pSAM360CompanyUser));
            string encodedPasswordUrl = HttpUtility.UrlEncode(SAM360XOR(SAM360Provider.GetSAM360PasswordSharedKey(), pSAM360CompanyPassword));

            return string.Format(SAM360Provider.GetSAM360UrlAuthentication(), encodedUserUrl, encodedPasswordUrl);
        }

        private string SAM360XOR(string key, string input)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
                sb.Append((char)(input[i] ^ key[(i % key.Length)]));

            string result = sb.ToString();

            return result;
        }

        
        #endregion

    }
}
