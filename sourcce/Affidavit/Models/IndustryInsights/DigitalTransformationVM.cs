using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class DigitalTransformationVM
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }

        public int Priority { get; set; }
        public bool Exist { get; set; }

        [Required(ErrorMessage = "The Pillar field is required")]
        public string Pillar { get; set; }
        [Required(ErrorMessage = "The Technology field is required")]
        public string Technology { get; set; }
        [Required(ErrorMessage = "The Brand field is required")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "The Comment field is required")]
        public string Comment { get; set; }
        [MaxLength(100, ErrorMessage = "{0} must have at least {1} characters")]
        public string Solution { get; set; }

        public bool Solved { get; set; }

        public Guid UserCreation { get; set; }
        public DateTime DateCreation { get; set; }
        public Guid UserLastModification { get; set; }
        public DateTime DateLastModification { get; set; }
    }
}