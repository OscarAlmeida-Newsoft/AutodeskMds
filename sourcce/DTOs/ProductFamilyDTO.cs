using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProductFamilyDTO
    {
        public short ProductFamilyID { get; set; }
        public string ProductFamilyName { get; set; }
        public string ProductFamilyImage { get; set; }
        public string ProductFamilyImageLarge { get; set; }
        public int ProductFamilyImageLargeWidth { get; set; }
        public int ProductFamilyImageLargeHeight { get; set; }
        public int OrderFamily { get; set; }

        public IEnumerable<ProductDTO> Products { get; set; }
        
    }
}
