using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_TblActivity
    {
        public int Id { get; set; }
        public int TypeActivityId { get; set; }
        public int TranslatorAdministratorDescriptionId { get; set; }
        public int TranslatorAdministratorDefinitionId { get; set; }
        public int TranslatorAdministratorExampleId { get; set; }
        public int Order { get; set; }
        public string ImageName { get; set; }

        public virtual NS_TblTypeActivity TypeActivity { get; set; }
        
    }
}
