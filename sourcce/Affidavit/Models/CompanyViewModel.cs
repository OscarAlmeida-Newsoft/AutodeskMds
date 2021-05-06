using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Affidavit.Utils;
using Affidavit.Attribute;

namespace Affidavit.Models
{

    public enum TipoCliente
    {
        Comercial = 1,
        Partner = 2,
        Academic = 3,
        DeveloperPartner = 4,
        PublicSector = 5
    }

    //public enum ProcesoCompra
    //{
    //    RegisteredName = 1,
    //    NaturalPerson = 2,
    //    TradeName = 3
    //}

    public class CompanyViewModel
    {
        [Required(ErrorMessage = "Please enter the user's first name.")]
        [TranslatorDisplay("Old_LabelNombreEmpresa")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Please enter the user's contact name.")]
        [TranslatorDisplay("Old_LabelNombreContacto")]
        public string NombreContacto { get; set; }

        [TranslatorRequired("Old_FieldRequired")]
        [TranslatorDisplay("Old_LabelIndustria")]
        public SelectList Industria { get; set; }

        [Required(ErrorMessage = "Please enter the user's email.")]
        //[EmailAddress(ErrorMessageResourceName = "EmailAddressField", ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessage = null)]
        [TranslatorDisplay("Old_LabelEmail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the user's phone number.")]
        [TranslatorDisplay("Old_LabelTelefono")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessageResourceName = "PhoneNumberField", ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessage = null)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessageResourceName = "PhoneNumberField")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Please enter the user's date.")]
        [TranslatorDisplay("Old_LabelFecha")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? Fecha { get; set; }
        
        [TranslatorDisplay("Old_LabelNumeroTotalEmpleados")]
        public int? NumeroTotalEmpleados { get; set; }

        [TranslatorDisplay("Old_LabelNumeroPCsEscritorio")]
        public short? NumeroPCsEscritorio { get; set; }
        
        [TranslatorDisplay("Old_LabelNumeroPortatiles")]
        public short? NumeroPortatiles { get; set; }

        [TranslatorDisplay("Old_LabelNumeroPCsOtrosSO")]
        public short? NumeroPCsOtrosSO { get; set; }

        [TranslatorDisplay("Old_LabelNumeroServidoresFisicos")]
        public short? NumeroServidoresFisicos { get; set; }

        [TranslatorDisplay("Old_LabelNumeroServidoresVirtuales")]
        public short? NumeroServidoresVirtuales { get; set; }

        [TranslatorDisplay("Old_LabelNumeroServidoresSQL")]
        public short? NumeroServidoresSQL { get; set; }

        [TranslatorDisplay("Old_LabelNombreExactoFacturacion")]
        public string NombreExactoFacturacion { get; set; }
        public List<string> ContratosPorVolumen { get; set; }


        public short IndustryID { get; set; }
        public byte? CompanyTypeID { get; set; }

        public TipoCliente TipoCliente { get; set; }
        public bool ProcesoCompra { get; set; }
        public SelectList Industries { get; set; }

        public IEnumerable<CompetenciasViewModel> Competencias { get; set; }
        public IEnumerable<ProgramasViewModel> Programas { get; set; }
        public List<string> CompetenciasLevel { get; set; }
        public List<string> CompetenciasPartnerID { get; set; }
        public List<DateTime?> CompetenciasDate { get; set; }
        public List<string> ProgramasPartnerID { get; set; }
        public List<DateTime?> ProgramasDate { get; set; }
        public int CompanyID { get; set; }
        public short CampaignID { get; set; }
        public Guid LeadID { get; set; }
        public bool IsFinal { get; set; }
        public int CurrentTab { get; set; }
        public bool IsLogOut { get; set; }
        public bool? AzureFlag { get; set; }

        public short? AcademicQttyTeachersPartialTime { get; set; }

        public short? AcademicQttyTeachersFullTime { get; set; }

        public short? AcademicQttyAdmEmpPartialTime { get; set; }

        public short? AcademicQttyAdmEmpFullTime { get; set; }

        public List<PartnerCapabilityCompanyDTO> CapabilityCompanyDTOList { get; set; }
        public List<PartnerProgramCompanyDTO> ProgramCompanyDTOList { get; set; }
        //public PartnerInfoViewModel PartnerInfo { get; set; }
    }
}