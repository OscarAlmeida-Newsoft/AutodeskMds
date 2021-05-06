using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_TblMaturityLevel
    {
        public string MaturityLevelId { get; set; }
        public int TranslatorAdministratorId { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        
    }
}
