using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    //public enum TipoDeCliente
    //{
    //    Comercial = 1,
    //    Partner = 2,
    //    Academic = 3,
    //    DeveloperPartner = 4,
    //    PublicSector = 5
    //}

    public enum EstadoLicenciamiento
    {
        Correcto = 1,
        Diferencias = 2,
        Apoyo = 3
    }

    public class AdditionalInfoViewModel
    {       

        public bool? Assurance { get; set; }
        public bool? NewLinceses { get; set; }
        public bool? UpgrateLicenses { get; set; }
        public bool? AutMicChannel { get; set; }
        public bool? CustomOrBasicsApplications { get; set; }
        public string EstadoContrato { get; set; }
        public string ExpliqueNewLinc { get; set; }
        public string ExpliqueUpgrateLinc { get; set; }
        public string AutChannel { get; set; }
        public string ContactNameCanalAutorizado { get; set; }
        [MaxLength(50)]
        public string TelefonoCanalAutorizado { get; set; }
        public string EmailCanalAutorizado { get; set; }
        public string DevelopersPartnersApplicationsType { get; set; }
        public string DevelopersPartnersMicrosofTools { get; set; }
        public string DevelopersPartnersProjectsTypes { get; set; }
        public bool IsFinal { get; set; }

        public TipoCliente TipoDeCliente { get; set; }

        public EstadoLicenciamiento EstLicenciamiento { get; set; }
    }
}