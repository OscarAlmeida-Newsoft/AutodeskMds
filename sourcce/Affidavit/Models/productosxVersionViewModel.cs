using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class productosxVersionViewModel
    {
        public int FamiliaID { get; set; }
        public string Nombre { get; set; }
        public string Comentario { get; set; }
        public string Image { get; set; }
        public string ImageLarge { get; set; }
        public int ImageLargeWidth { get; set; }
        public int ImageLargeHeight { get; set; }
        public bool FamilyHasCAL { get; set; }
        public IEnumerable<Versiones> Versiones { get; set; }
        public IEnumerable<Productos> Productos { get; set; }
    }

    public class Versiones
    {
        public string version { get; set; }
        public bool Flag { get; set; }
    }

    public class Productos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreDisplay { get; set; }
        public string Version { get; set; }
        public string ProductVersion { get; set; }
        public short InstalledLicenses { get; set; }
        public short FilesAmount { get; set; }
        public short VLS { get; set; }
        public short FPP { get; set; }   
        public short OEM { get; set; }   
        public short WEB { get; set; }
        public string Agreement { get; set; }
        public short? DisplayOrder { get; set; }
        public bool IsCal { get; set; }

    }
}