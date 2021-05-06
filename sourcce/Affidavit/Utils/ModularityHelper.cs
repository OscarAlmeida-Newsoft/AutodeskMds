using Affidavit.Helpers;
using DTOs;
using DTOs.Utils;
using IServices;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Utils
{
    public static class ModularityHelper
    {
        private static ICRMService _crmService
        {
            get
            {
                ICRMService _crmServiceResolve = DependencyResolver.Current.GetService<ICRMService>();
                return _crmServiceResolve;
            }
        }

        private static ISessionState _sessionState
        {
            get
            {
                ISessionState _sessionStateResolve = DependencyResolver.Current.GetService<ISessionState>();
                return _sessionStateResolve;
            }
        }

        public static bool CanISeeV3(Guid pLeadID)
        {
            CRMDataDTO _info = _crmService.GetLeadByID(pLeadID);
            string _allowedCountriesToSeeV3 = ConfigurationManager.AppSettings["AllowedCountriesToSeeV3"];
            string[] _countriesList = _allowedCountriesToSeeV3.Split(',');

            if (_countriesList != null && _countriesList.Length > 0 && _countriesList.Contains(_info.CountryName))
            {
                return true;
            }
            else if (_info.LeadID == Guid.Parse("C3C7EAA1-2E92-E611-80F7-5065F38B0201") ||
               _info.LeadID == Guid.Parse("C5C7EAA1-2E92-E611-80F7-5065F38B0201") ||
               _info.LeadID == Guid.Parse("C7C7EAA1-2E92-E611-80F7-5065F38B0201") ||
               _info.LeadID == Guid.Parse("C9C7EAA1-2E92-E611-80F7-5065F38B0201") ||
               _info.LeadID == Guid.Parse("CBC7EAA1-2E92-E611-80F7-5065F38B0201") ||
               _info.LeadID == Guid.Parse("CDC7EAA1-2E92-E611-80F7-5065F38B0201") ||
               _info.LeadID == Guid.Parse("CFC7EAA1-2E92-E611-80F7-5065F38B0201") ||
               _info.LeadID == Guid.Parse("D1C7EAA1-2E92-E611-80F7-5065F38B0201") ||
               _info.LeadID == Guid.Parse("D3C7EAA1-2E92-E611-80F7-5065F38B0201") ||
               _info.LeadID == Guid.Parse("D5C7EAA1-2E92-E611-80F7-5065F38B0201") ||
               _info.LeadID == Guid.Parse("D7C7EAA1-2E92-E611-80F7-5065F38B0201") ||
               _info.LeadID == Guid.Parse("D9C7EAA1-2E92-E611-80F7-5065F38B0201")
               )
            {
                return true;
            }

            return false;
        }

        public static bool CanISeeV3()
        {
            Guid _leadId = Guid.Parse(_sessionState.Get<string>(ConstantsStringKeys.CURRENT_LEADID__KEY));

            CRMDataDTO _info = _crmService.GetLeadByID(_leadId);

            string _allowedCountriesToSeeV3 = ConfigurationManager.AppSettings["AllowedCountriesToSeeV3"];
            string[] _countriesList = _allowedCountriesToSeeV3.Split(',');

            if(_countriesList != null && _countriesList.Length > 0 && _countriesList.Contains(_info.CountryName))
            {
                return true;
            }else if (_leadId == Guid.Parse("C3C7EAA1-2E92-E611-80F7-5065F38B0201") ||
                _leadId == Guid.Parse("C5C7EAA1-2E92-E611-80F7-5065F38B0201") ||
                _leadId == Guid.Parse("C7C7EAA1-2E92-E611-80F7-5065F38B0201") ||
                _leadId == Guid.Parse("C9C7EAA1-2E92-E611-80F7-5065F38B0201") ||
                _leadId == Guid.Parse("CBC7EAA1-2E92-E611-80F7-5065F38B0201") ||
                _leadId == Guid.Parse("CDC7EAA1-2E92-E611-80F7-5065F38B0201") ||
                _leadId == Guid.Parse("CFC7EAA1-2E92-E611-80F7-5065F38B0201") ||
                _leadId == Guid.Parse("D1C7EAA1-2E92-E611-80F7-5065F38B0201") ||
                _leadId == Guid.Parse("D3C7EAA1-2E92-E611-80F7-5065F38B0201") ||
                _leadId == Guid.Parse("D5C7EAA1-2E92-E611-80F7-5065F38B0201") ||
                _leadId == Guid.Parse("D7C7EAA1-2E92-E611-80F7-5065F38B0201") ||
                _leadId == Guid.Parse("D9C7EAA1-2E92-E611-80F7-5065F38B0201")
                )
            {
                return true;
            }



            return false;
             
        }
    }
}