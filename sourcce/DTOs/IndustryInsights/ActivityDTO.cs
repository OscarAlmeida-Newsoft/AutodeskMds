using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ActivityDTO
    {
        public int Id { get; set; }
        public int TypeActivityId { get; set; }
        public int TranslatorAdministratorDescriptionId { get; set; }
        public int TranslatorAdministratorDefinitionId { get; set; }
        public int TranslatorAdministratorExampleId { get; set; }
        public int Order { get; set; }
        public string ImageName { get; set; }

        //Scoring
        public decimal Score { get; set; }
        public bool TieneProcesos { get; set; }
        public string ClassCss { get; set; }
    }
}
