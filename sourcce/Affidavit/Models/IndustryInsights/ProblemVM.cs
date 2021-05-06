using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Affidavit.Models
{
    public class ProblemVM
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }

        [Required(ErrorMessage = "The Description field is required")]
        public string ProblemDescription { get; set; }
        [Required(ErrorMessage = "The Opportunity field is required")]
        public string Opportunity { get; set; }
        [Required(ErrorMessage = "The Technology field is required")]
        public string Technology { get; set; }
        [Required(ErrorMessage = "The Inventory field is required")]
        public string Inventory { get; set; }

        public int Priority { get; set; }
        public bool Exist { get; set; }
        public bool Microsoft { get; set; }
        [MaxLength(100, ErrorMessage = "{0} must have at least {1} characters")]
        public string Solution { get; set; }

        public bool Solved { get; set; }

        public Guid UserCreation { get; set; }
        public DateTime DateCreation { get; set; }
        public Guid UserLastModification { get; set; }
        public DateTime DateLastModification { get; set; }
    }
}