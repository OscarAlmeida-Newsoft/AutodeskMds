using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class FamilyViewModel
    {
        //public int ProductFamilyID { get; set; }
        //public string ProductFamilyName { get; set; }
        public IEnumerable<productosxVersionViewModel> Families { get; set; }

        public bool? AzureFlag { get; set; }

        public List<short> ProductosInstalledLicenses { get; set; }
        public List<short> ProductosVLS { get; set; }
        public List<short> ProductosFPP { get; set; }
        public List<short> ProductosOEM { get; set; }
        public List<short> ProductosWEB { get; set; }
        //public List<string> Contratos { get; set; }
        public List<string> ProductosComentariosAdicionales { get; set; }
        public IEnumerable<string> NombreProductos { get; set; }
        public List<short> IDProductos { get; set; }
        public List<short> IDProductosWEB { get; set; }
        public List<short> IDProductosOEM { get; set; }
        //public List<short> IDContratos { get; set; }
        public List<byte> IDFamilyList { get; set; }
        public int CompanyID { get; set; }
        public short CampaignID { get; set; }
        public Guid LeadID { get; set; }
        public bool IsFinal { get; set; }
    }
}