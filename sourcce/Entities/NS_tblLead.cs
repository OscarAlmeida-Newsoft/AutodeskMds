namespace Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class NS_tblLead
    {
        [Key]
        [StringLength(50)]
        public string CRMLeadID { get; set; }

        [StringLength(100)]
        public string LeadName { get; set; }

        public DateTime? AffidavitReceivedDate { get; set; }
    }
}
