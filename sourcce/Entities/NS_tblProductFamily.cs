namespace Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class NS_tblProductFamily
    {
        public NS_tblProductFamily()
        {
            NS_tblProductFamilyCompany = new HashSet<NS_tblProductFamilyCompany>();
            NS_tblProduct = new HashSet<NS_tblProduct>();
            NS_tblQuestionxProductFamily = new HashSet<NS_tblQuestionxProductFamily>();
        }

        [Key]
        public byte ProductFamilyID { get; set; }

        [StringLength(50)]
        public string ProductFamilyName { get; set; }
        public string ProductFamilyImage { get; set; }
        public string ProductFamilyImageLarge { get; set; }
        public int ProductFamilyImageLargeWidth { get; set; }
        public int ProductFamilyImageLargeHeight { get; set; }
        public int OrderFamily { get; set; }

        public virtual ICollection<NS_tblProductFamilyCompany> NS_tblProductFamilyCompany { get; set; }
        public virtual ICollection<NS_tblProduct> NS_tblProduct { get; set; }
        public virtual ICollection<NS_tblQuestionxProductFamily> NS_tblQuestionxProductFamily { get; set; }
    }
}
