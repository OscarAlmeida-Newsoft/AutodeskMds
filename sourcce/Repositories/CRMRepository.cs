using IRepositories;
using System;


using System.ServiceModel;

// These namespaces are found in the Microsoft.Crm.Sdk.Proxy.dll assembly
// located in the SDK\bin folder of the SDK download.
//using Microsoft.Crm.Sdk.Messages;

// These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
// located in the SDK\bin folder of the SDK download.
//using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

// These namespaces are found in the Microsoft.Xrm.Client.dll assembly
// located in the SDK\bin folder of the SDK download.
//using Microsoft.Xrm.Client;
//using Microsoft.Xrm.Client.Services;
using Entities;
using DTOs;
using System.Collections.Generic;
using System.Linq;
//NEW Dynamics CRM 365 Connection 
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System.ServiceModel.Description;



namespace Repositories
{
    public class CRMRepository : ICRMRepository
    {
        private IOrganizationService _orgService;
        readonly ICRMConnectionProvider _crmConnectionProvider;
        public CRMRepository(ICRMConnectionProvider pCrmConnectionProvicer )
        {
            _crmConnectionProvider = pCrmConnectionProvicer;
        }

        public string GetCRMEntityValue()
        {

            CrmServiceClient crmSvc = new CrmServiceClient(_crmConnectionProvider.GetCRMConnectionString());

            _orgService = (IOrganizationService)crmSvc.OrganizationWebProxyClient != null ?
                (IOrganizationService)crmSvc.OrganizationWebProxyClient : (IOrganizationService)crmSvc.OrganizationServiceProxy;


            //OrganizationService _orgService;
            String CompanyName;

            //// Establish a connection to the organization web service
            //Microsoft.Xrm.Client.CrmConnection connection = CrmConnection.Parse("Url=https://echezinc.crm.dynamics.com;Username = new.soft@e-chez.com; Password = Yosu4082; ");


            Account retrievedAccount = (Account)_orgService.Retrieve("account", new Guid("9DD6B5D6-F7E5-E511-80F3-3863BB346888"), new ColumnSet(true));
            CompanyName = retrievedAccount.Name;

            //using (_orgService = new OrganizationService(connection))
            //{
            //    Account retrievedAccount = (Account)_orgService.Retrieve("account", new Guid("9DD6B5D6-F7E5-E511-80F3-3863BB346888"), new ColumnSet(true));

            //    CompanyName = retrievedAccount.Name;

            //}

            //throw new NotImplementedException();

            return CompanyName;
        }

        /// <summary>
        ///     Repositorio para consultar un lead por su id.
        /// </summary>
        /// <param name="pLeadID">Id del lead a consultar</param>
        /// <returns></returns>
        public CRMDataDTO GetLeadByID(Guid pLeadID)
        {
            //OrganizationService _orgService;
            String CompanyLeadName;
            Lead retrievedLead;
            CRMDataDTO _dataDTO = new CRMDataDTO();

            //// Establish a connection to the organization web service
            //Microsoft.Xrm.Client.CrmConnection connection = CrmConnection.Parse(Dts.Variables["User::DynamicsCRMConnectionString"].Value.ToString());
            //Microsoft.Xrm.Client.CrmConnection connection = CrmConnection.Parse("Url=https://echezinc.crm.dynamics.com;Username = new.soft@e-chez.com; Password = Yosu4082; ");

            CrmServiceClient crmSvc = new CrmServiceClient(_crmConnectionProvider.GetCRMConnectionString());
            _orgService = (IOrganizationService)crmSvc.OrganizationWebProxyClient != null ?
                (IOrganizationService)crmSvc.OrganizationWebProxyClient : (IOrganizationService)crmSvc.OrganizationServiceProxy;



            _dataDTO.LeadID = pLeadID;
            retrievedLead = (Lead)_orgService.Retrieve("lead", pLeadID, new ColumnSet(true));

            if (retrievedLead.ParentAccountId != null)
            {

                Account retrievedAccount = (Account)_orgService.Retrieve("account", retrievedLead.ParentAccountId.Id, new ColumnSet(true));

                _dataDTO.IndustryName = (retrievedAccount.new_industry != null) ? retrievedAccount.FormattedValues["new_industry"].ToString() : "";

                _dataDTO.AccountNumber = retrievedLead.ParentAccountId.Id.ToString();
            }
            else
            {
                _dataDTO.IndustryName = "Unknown";
            }

            //_dataDTO.CountryName =  (retrievedAccount.new_country != null) ? retrievedAccount.new_country.Name : "";

            _dataDTO.CountryName = (retrievedLead.new_country != null) ? retrievedLead.new_country.Name : "";

            _dataDTO.Email = retrievedLead.EMailAddress1;

            _dataDTO.PhoneNumber = retrievedLead.Telephone1;

            _dataDTO.ContactName = retrievedLead.FullName;

            _dataDTO.CompanyName = retrievedLead.CompanyName;

            _dataDTO.CRMCampaignID = retrievedLead.CampaignId.Id.ToString();

            _dataDTO.CampaignName = retrievedLead.CampaignId.Name;

            _dataDTO.FirstName = retrievedLead.FirstName;

            _dataDTO.MobilePhone = retrievedLead.MobilePhone;

            _dataDTO.LastName = retrievedLead.LastName;

            _dataDTO.ConsultantName = (retrievedLead.Attributes.Contains("new_consultant")) ? ((EntityReference)(retrievedLead.Attributes["new_consultant"])).Name.ToString() : "";

            _dataDTO.NumberOfEmployees = (retrievedLead.Attributes.Contains("numberofemployees")) ? Convert.ToInt32((retrievedLead.Attributes["numberofemployees"])) : 0;
            _dataDTO.NumberOfPCs = (retrievedLead.Attributes.Contains("new_numberpc")) ? Convert.ToInt32((retrievedLead.Attributes["new_numberpc"])) : 0;
            //retrieve and assign the lead consultant information
            Guid _consultantId = (retrievedLead.Attributes.Contains("new_consultant")) ? ((EntityReference)(retrievedLead.Attributes["new_consultant"])).Id : Guid.Empty;

            if (_consultantId != Guid.Empty)
            {
                var user = _orgService.Retrieve(SystemUser.EntityLogicalName, _consultantId, new ColumnSet(true));
                _dataDTO.MicrosoftConsultantPhoneNumber = (user.Attributes.Contains("address1_telephone2")) ? (user.Attributes["address1_telephone2"]).ToString() : "";
                _dataDTO.MicrosoftConsultantEmail = (user.Attributes.Contains("personalemailaddress")) ? (user.Attributes["personalemailaddress"]).ToString() : "";
                //_dataDTO.Tittle = (_leadData.Attributes.Contains("jobtitle")) ? (_leadData.Attributes["jobtitle"]).ToString() : "";
                _dataDTO.Tittle = (user.Attributes.Contains("jobtitle")) ? (user.Attributes["jobtitle"]).ToString() : "";
            }

            //CompanyLeadName = string.Concat(LeadName, " " + CompanyName);

            // NOTA: Hacer debug y revisar el contenido de la variable retrievedLead, ahi se ven todos los campos del LEAD




            //using (_orgService = new OrganizationService(connection))
            //{
            //    _dataDTO.LeadID = pLeadID;
            //    retrievedLead = (Lead)_orgService.Retrieve("lead", pLeadID, new ColumnSet(true));

            //    if (retrievedLead.ParentAccountId != null)
            //    {
            //        Account retrievedAccount = (Account)_orgService.Retrieve("account", retrievedLead.ParentAccountId.Id, new ColumnSet(true));

            //        _dataDTO.IndustryName = (retrievedAccount.new_industry != null) ? retrievedAccount.FormattedValues["new_industry"].ToString() : "";

            //        _dataDTO.AccountNumber = retrievedLead.ParentAccountId.Id.ToString();
            //    }
            //    else
            //    {
            //        _dataDTO.IndustryName = "Unknown";
            //    }

            //    //_dataDTO.CountryName =  (retrievedAccount.new_country != null) ? retrievedAccount.new_country.Name : "";

            //    _dataDTO.CountryName = (retrievedLead.new_country != null) ? retrievedLead.new_country.Name : "";

            //    _dataDTO.Email = retrievedLead.EMailAddress1;

            //    _dataDTO.PhoneNumber = retrievedLead.Telephone1;

            //    _dataDTO.ContactName = retrievedLead.FullName;

            //    _dataDTO.CompanyName = retrievedLead.CompanyName;

            //    _dataDTO.CRMCampaignID = retrievedLead.CampaignId.Id.ToString();

            //    _dataDTO.CampaignName = retrievedLead.CampaignId.Name;

            //    _dataDTO.LastName = retrievedLead.LastName;

            //    _dataDTO.ConsultantName = (retrievedLead.Attributes.Contains("new_consultant")) ? ((EntityReference)(retrievedLead.Attributes["new_consultant"])).Name.ToString() : "";

            //    _dataDTO.NumberOfEmployees = (retrievedLead.Attributes.Contains("numberofemployees")) ? Convert.ToInt32((retrievedLead.Attributes["numberofemployees"])) : 0;
            //    _dataDTO.NumberOfPCs = (retrievedLead.Attributes.Contains("new_numberpc")) ? Convert.ToInt32((retrievedLead.Attributes["new_numberpc"])) : 0;
            //    //retrieve and assign the lead consultant information
            //    Guid _consultantId = (retrievedLead.Attributes.Contains("new_consultant")) ? ((EntityReference)(retrievedLead.Attributes["new_consultant"])).Id : Guid.Empty;

            //    if (_consultantId != Guid.Empty)
            //    {
            //        var user = _orgService.Retrieve(SystemUser.EntityLogicalName, _consultantId, new ColumnSet(true));
            //        _dataDTO.MicrosoftConsultantPhoneNumber = (user.Attributes.Contains("address1_telephone2")) ? (user.Attributes["address1_telephone2"]).ToString() : "";
            //        _dataDTO.MicrosoftConsultantEmail = (user.Attributes.Contains("personalemailaddress")) ? (user.Attributes["personalemailaddress"]).ToString() : "";
            //        //_dataDTO.Tittle = (_leadData.Attributes.Contains("jobtitle")) ? (_leadData.Attributes["jobtitle"]).ToString() : "";
            //        _dataDTO.Tittle = (user.Attributes.Contains("jobtitle")) ? (user.Attributes["jobtitle"]).ToString() : "";
            //    }

            //    //CompanyLeadName = string.Concat(LeadName, " " + CompanyName);

            //    // NOTA: Hacer debug y revisar el contenido de la variable retrievedLead, ahi se ven todos los campos del LEAD
            //}

            return _dataDTO;
        }

        /// <summary>
        /// Get lead information for survey question consultant page
        /// </summary>
        /// <param name="pCompanyName">Name of the company</param>
        /// <returns></returns>
        public List<CRMDataDTO> GetLeadInfoByCompanyName(string pCompanyName)
        {
            //OrganizationService _orgService;
            List<CRMDataDTO> _lead = new List<CRMDataDTO>();

            //Connection to CRM System configuration
            //CrmConnection connection = CrmConnection.Parse(
            //    string.Format(CONNECTION_STRING, CRMConnectionProvider.GetUrlConnection()
            //    , CRMConnectionProvider.GetUserConnection()
            //    , CRMConnectionProvider.GetSecureConnection())
            //    );
            //Microsoft.Xrm.Client.CrmConnection connection = CrmConnection.Parse("Url=https://echezinc.crm.dynamics.com;Username = new.soft@e-chez.com; Password = Yosu4082; ");

            CrmServiceClient crmSvc = new CrmServiceClient(_crmConnectionProvider.GetCRMConnectionString());
            _orgService = (IOrganizationService)crmSvc.OrganizationWebProxyClient != null ?
                (IOrganizationService)crmSvc.OrganizationWebProxyClient : (IOrganizationService)crmSvc.OrganizationServiceProxy;


            QueryExpression query = new QueryExpression()
            {
                Distinct = false,
                EntityName = Lead.EntityLogicalName,
                ColumnSet = new ColumnSet(true),
                Criteria =
                    {
                         Filters =
                        {
                            new FilterExpression
                            {
                                Conditions =
                                {
                                    new ConditionExpression("companyname", ConditionOperator.Like, "%"+pCompanyName+"%")
                                }
                            }
                        }
                    }
            };

            DataCollection<Entity> LeadCollection = _orgService.RetrieveMultiple(query).Entities;

            foreach (Lead lead in LeadCollection)
            {
                _lead.Add(new CRMDataDTO
                {
                    LeadID = lead.Id,
                    CompanyName = lead.CompanyName,
                    CampaignName = (lead.CampaignId == null) ? "--" : lead.CampaignId.Name
                });
            }

            //QueryExpression qe = new QueryExpression()
            //{
            //    Distinct = false,
            //    EntityName = "account",
            //    ColumnSet = new ColumnSet("accountid")

            //};

            //qe.Criteria.AddCondition("companyname", ConditionOperator.Contains, pCompanyName);

            //LinkEntity _link = new LinkEntity
            //{
            //    LinkFromEntityName = "account",
            //    LinkToEntityName = "lead",
            //    LinkFromAttributeName = "accountId",
            //    LinkToAttributeName = "accountId"
            //};

            //_link.LinkCriteria.AddCondition("LeadId");
            //using (_orgService = new OrganizationService(connection))
            //{

            //    EntityCollection _result = _orgService.RetrieveMultiple(qe);
            //}


            return _lead;




            //using (_orgService = new OrganizationService(connection))
            //{
            //    QueryExpression query = new QueryExpression()
            //    {
            //        Distinct = false,
            //        EntityName = Lead.EntityLogicalName,
            //        ColumnSet = new ColumnSet(true),
            //        Criteria =
            //        {
            //             Filters =
            //            {
            //                new FilterExpression
            //                {
            //                    Conditions =
            //                    {
            //                        new ConditionExpression("companyname", ConditionOperator.Like, "%"+pCompanyName+"%")
            //                    }
            //                }
            //            }
            //        }                    
            //    };

            //    DataCollection<Entity> LeadCollection = _orgService.RetrieveMultiple(query).Entities;

            //    foreach (Lead lead in LeadCollection)
            //    {
            //        _lead.Add(new CRMDataDTO
            //        {
            //            LeadID = lead.Id,
            //            CompanyName = lead.CompanyName,
            //            CampaignName = (lead.CampaignId == null) ? "--" : lead.CampaignId.Name
            //        });
            //    }

            //    //QueryExpression qe = new QueryExpression()
            //    //{
            //    //    Distinct = false,
            //    //    EntityName = "account",
            //    //    ColumnSet = new ColumnSet("accountid")

            //    //};

            //    //qe.Criteria.AddCondition("companyname", ConditionOperator.Contains, pCompanyName);

            //    //LinkEntity _link = new LinkEntity
            //    //{
            //    //    LinkFromEntityName = "account",
            //    //    LinkToEntityName = "lead",
            //    //    LinkFromAttributeName = "accountId",
            //    //    LinkToAttributeName = "accountId"
            //    //};

            //    //_link.LinkCriteria.AddCondition("LeadId");
            //    //using (_orgService = new OrganizationService(connection))
            //    //{

            //    //    EntityCollection _result = _orgService.RetrieveMultiple(qe);
            //    //}


            //    return _lead;
            //}
        }

        /// <summary>
        /// Get lead information for survey question consultant page
        /// </summary>
        /// <param name="pCompanyName">Name of the company</param>
        /// <param name="pConsultant">Name of the consultant</param>
        /// <returns></returns>
        public List<CRMDataDTO> GetLeadInfoByCompanyNameAndConsultant(string pCompanyName, string pConsultant)
        {
            //OrganizationService _orgService;
            List<CRMDataDTO> _lead = new List<CRMDataDTO>();

            //Microsoft.Xrm.Client.CrmConnection connection = CrmConnection.Parse("Url=https://echezinc.crm.dynamics.com;Username = new.soft@e-chez.com; Password = Yosu4082; ");
            CrmServiceClient crmSvc = new CrmServiceClient(_crmConnectionProvider.GetCRMConnectionString());
            _orgService = (IOrganizationService)crmSvc.OrganizationWebProxyClient != null ?
                (IOrganizationService)crmSvc.OrganizationWebProxyClient : (IOrganizationService)crmSvc.OrganizationServiceProxy;

            QueryExpression query = new QueryExpression()
            {
                Distinct = false,
                EntityName = Lead.EntityLogicalName,
                ColumnSet = new ColumnSet(true),
                LinkEntities =
                    {
                        //new LinkEntity
                        //{
                        //    JoinOperator = JoinOperator.Inner,
                        //    LinkFromEntityName = Lead.EntityLogicalName,
                        //    LinkToEntityName = Account.EntityLogicalName,
                        //    LinkFromAttributeName = "parentaccountid",
                        //    LinkToAttributeName = "accountid",
                        //    Columns = new ColumnSet(true),
                        //    LinkCriteria =
                        //    {
                        //        Conditions =
                        //        {
                        //            // Aquí va el texto que el cliente da en la vista, el nombre de la compañía
                        //            // que desea buscar. El "name" es el nombre de la compañía en la entidad Account
                        //            new ConditionExpression("name", ConditionOperator.Like, "%"+pCompanyName+"%")
                        //        }
                        //    }
                        //},
                        new LinkEntity
                        {
                            JoinOperator = JoinOperator.Inner,
                            LinkFromEntityName = Lead.EntityLogicalName,
                            LinkToEntityName = SystemUser.EntityLogicalName,
                            LinkFromAttributeName = "new_consultant",//Campo de Lead
                            LinkToAttributeName = "systemuserid", //Campo de SystemUser
                            Columns = new ColumnSet(true),
                            LinkCriteria =
                            {
                                Conditions =
                                {
                                    new ConditionExpression("internalemailaddress", ConditionOperator.Equal, pConsultant) //LEAD (COrreo de e-chez)
                                }
                            }
                        }
                    }
            };

            query.Criteria.AddCondition("companyname", ConditionOperator.Like, "%" + pCompanyName + "%");

            //var user = _orgService.Retrieve(SystemUser.EntityLogicalName, _consultantId, new ColumnSet(true));
            DataCollection<Entity> LeadCollection = _orgService.RetrieveMultiple(query).Entities;

            foreach (Lead lead in LeadCollection)
            {
                _lead.Add(new CRMDataDTO
                {
                    LeadID = lead.Id,
                    CompanyName = lead.CompanyName,
                    CampaignName = (lead.CampaignId == null) ? "--" : lead.CampaignId.Name,
                    ConsultantName = (lead.Attributes.Contains("new_consultant")) ? ((EntityReference)(lead.Attributes["new_consultant"])).Name.ToString() : "",
                });
            }

            return _lead;

            //using (_orgService = new OrganizationService(connection))
            //{
            //    QueryExpression query = new QueryExpression()
            //    {
            //        Distinct = false,
            //        EntityName = Lead.EntityLogicalName,
            //        ColumnSet = new ColumnSet(true),
            //        LinkEntities =
            //        {
            //            //new LinkEntity
            //            //{
            //            //    JoinOperator = JoinOperator.Inner,
            //            //    LinkFromEntityName = Lead.EntityLogicalName,
            //            //    LinkToEntityName = Account.EntityLogicalName,
            //            //    LinkFromAttributeName = "parentaccountid",
            //            //    LinkToAttributeName = "accountid",
            //            //    Columns = new ColumnSet(true),
            //            //    LinkCriteria =
            //            //    {
            //            //        Conditions =
            //            //        {
            //            //            // Aquí va el texto que el cliente da en la vista, el nombre de la compañía
            //            //            // que desea buscar. El "name" es el nombre de la compañía en la entidad Account
            //            //            new ConditionExpression("name", ConditionOperator.Like, "%"+pCompanyName+"%")
            //            //        }
            //            //    }
            //            //},
            //            new LinkEntity
            //            {
            //                JoinOperator = JoinOperator.Inner,
            //                LinkFromEntityName = Lead.EntityLogicalName,
            //                LinkToEntityName = SystemUser.EntityLogicalName,
            //                LinkFromAttributeName = "new_consultant",
            //                LinkToAttributeName = "systemuserid",
            //                Columns = new ColumnSet(true),
            //                LinkCriteria =
            //                {
            //                    Conditions =
            //                    {
            //                        new ConditionExpression("internalemailaddress", ConditionOperator.Equal, pConsultant)
            //                    }
            //                }
            //            }
            //        }
            //    };

            //    query.Criteria.AddCondition("companyname", ConditionOperator.Like, "%" + pCompanyName + "%");

            //    //var user = _orgService.Retrieve(SystemUser.EntityLogicalName, _consultantId, new ColumnSet(true));
            //    DataCollection <Entity> LeadCollection = _orgService.RetrieveMultiple(query).Entities;

            //    foreach (Lead lead in LeadCollection)
            //    {                    
            //        _lead.Add(new CRMDataDTO
            //        {
            //            LeadID = lead.Id,
            //            CompanyName = lead.CompanyName,
            //            CampaignName = (lead.CampaignId == null) ? "--" : lead.CampaignId.Name,
            //            ConsultantName = (lead.Attributes.Contains("new_consultant")) ? ((EntityReference)(lead.Attributes["new_consultant"])).Name.ToString() : "",
            //        });                    
            //    }                

            //    return _lead;
            //}
        }

        /// <summary>
        /// check if exist a campaign in the CRM
        /// </summary>
        /// <param name="pCampaignName">Campaign Name to verified</param>
        /// <returns></returns>
        public bool CheckCampaign(string pCampaignName)
        {
            //OrganizationService _orgService;

            //Microsoft.Xrm.Client.CrmConnection connection = CrmConnection.Parse("Url=https://echezinc.crm.dynamics.com;Username = new.soft@e-chez.com; Password = Yosu4082; ");
            CrmServiceClient crmSvc = new CrmServiceClient(_crmConnectionProvider.GetCRMConnectionString());
            _orgService = (IOrganizationService)crmSvc.OrganizationWebProxyClient != null ?
                (IOrganizationService)crmSvc.OrganizationWebProxyClient : (IOrganizationService)crmSvc.OrganizationServiceProxy;

            QueryExpression query = new QueryExpression()
            {
                Distinct = false,
                EntityName = Campaign.EntityLogicalName,
                ColumnSet = new ColumnSet(true),
                Criteria =
                    {
                         Filters =
                        {
                            new FilterExpression
                            {
                                Conditions =
                                {
                                    new ConditionExpression("name", ConditionOperator.Equal, pCampaignName)
                                }
                            }
                        }
                    }
            };

            //var user = _orgService.Retrieve(SystemUser.EntityLogicalName, _consultantId, new ColumnSet(true));
            DataCollection<Entity> LeadCollection = _orgService.RetrieveMultiple(query).Entities;

            var _isExist = (LeadCollection.Count != 0) ? true : false;

            return _isExist;

            //using (_orgService = new OrganizationService(connection))
            //{
            //    QueryExpression query = new QueryExpression()
            //    {
            //        Distinct = false,
            //        EntityName = Campaign.EntityLogicalName,
            //        ColumnSet = new ColumnSet(true),
            //        Criteria =
            //        {
            //             Filters =
            //            {
            //                new FilterExpression
            //                {
            //                    Conditions =
            //                    {
            //                        new ConditionExpression("name", ConditionOperator.Equal, pCampaignName)
            //                    }
            //                }
            //            }
            //        }
            //    };

            //    //var user = _orgService.Retrieve(SystemUser.EntityLogicalName, _consultantId, new ColumnSet(true));
            //    DataCollection<Entity> LeadCollection = _orgService.RetrieveMultiple(query).Entities;

            //    var _isExist = (LeadCollection.Count != 0) ? true : false;

            //    return _isExist;
            //}
        }

        public string GetConsultantTitle(Guid pLeadID)
        {
            string _title = "";
            //OrganizationService _orgService;
            Lead retrievedLead;
            //Microsoft.Xrm.Client.CrmConnection connection = CrmConnection.Parse("Url=https://echezinc.crm.dynamics.com;Username = new.soft@e-chez.com; Password = Yosu4082; ");

            CrmServiceClient crmSvc = new CrmServiceClient(_crmConnectionProvider.GetCRMConnectionString());
            _orgService = (IOrganizationService)crmSvc.OrganizationWebProxyClient != null ?
                (IOrganizationService)crmSvc.OrganizationWebProxyClient : (IOrganizationService)crmSvc.OrganizationServiceProxy;
            retrievedLead = (Lead)_orgService.Retrieve("lead", pLeadID, new ColumnSet(true));

            Guid _consultantId = (retrievedLead.Attributes.Contains("new_consultant")) ? ((EntityReference)(retrievedLead.Attributes["new_consultant"])).Id : Guid.Empty;

            if (_consultantId != Guid.Empty)
            {
                var user = _orgService.Retrieve(SystemUser.EntityLogicalName, _consultantId, new ColumnSet(true));
                _title = (user.Attributes.Contains("title")) ? (user.Attributes["title"]).ToString() : "";
            }


            //using (_orgService = new OrganizationService(connection))
            //{
            //    retrievedLead = (Lead)_orgService.Retrieve("lead", pLeadID, new ColumnSet(true));

            //    Guid _consultantId = (retrievedLead.Attributes.Contains("new_consultant")) ? ((EntityReference)(retrievedLead.Attributes["new_consultant"])).Id : Guid.Empty;

            //    if (_consultantId != Guid.Empty)
            //    {
            //        var user = _orgService.Retrieve(SystemUser.EntityLogicalName, _consultantId, new ColumnSet(true));
            //        _title = (user.Attributes.Contains("title")) ? (user.Attributes["title"]).ToString() : "";
            //    }
            //}            

            return _title;
            
        }

        /// <summary>
        /// Get the consultant authorize region by email
        /// </summary>
        /// <param name="pEmailconsultant">Email consultant</param>
        /// <returns></returns>
        public string GetUserRegionByEmail(string pEmailconsultant)
        {
            string _businessUnitId = "";
            //OrganizationService _orgService;
            List<CRMDataDTO> _lead = new List<CRMDataDTO>();

            //Microsoft.Xrm.Client.CrmConnection connection = CrmConnection.Parse("Url=https://echezinc.crm.dynamics.com;Username = new.soft@e-chez.com; Password = Yosu4082; ");
            CrmServiceClient crmSvc = new CrmServiceClient(_crmConnectionProvider.GetCRMConnectionString());
            _orgService = (IOrganizationService)crmSvc.OrganizationWebProxyClient != null ?
                (IOrganizationService)crmSvc.OrganizationWebProxyClient : (IOrganizationService)crmSvc.OrganizationServiceProxy;


            QueryByAttribute querybyattribute = new QueryByAttribute("systemuser");

            querybyattribute.ColumnSet = new ColumnSet(true);

            //  Attribute to query.
            querybyattribute.Attributes.AddRange("internalemailaddress");

            //  Value of queried attribute to return.
            querybyattribute.Values.AddRange(pEmailconsultant);

            //  Query passed to service proxy.
            EntityCollection retrievedSysteUser = _orgService.RetrieveMultiple(querybyattribute);

            if (retrievedSysteUser.Entities.Count == 1)
            {
                var Row = (SystemUser)retrievedSysteUser[0];
                _businessUnitId = Row.BusinessUnitId.Name;
            }

            //using (_orgService = new OrganizationService(connection))
            //{
            //    QueryByAttribute querybyattribute = new QueryByAttribute("systemuser");

            //    querybyattribute.ColumnSet = new ColumnSet(true);

            //    //  Attribute to query.
            //    querybyattribute.Attributes.AddRange("internalemailaddress");

            //    //  Value of queried attribute to return.
            //    querybyattribute.Values.AddRange(pEmailconsultant);

            //    //  Query passed to service proxy.
            //    EntityCollection retrievedSysteUser = _orgService.RetrieveMultiple(querybyattribute);

            //    if (retrievedSysteUser.Entities.Count == 1)
            //    {
            //        var Row = (SystemUser)retrievedSysteUser[0];
            //        _businessUnitId = Row.BusinessUnitId.Name;
            //    }
            //}            

            return _businessUnitId;
        }
    }
}
