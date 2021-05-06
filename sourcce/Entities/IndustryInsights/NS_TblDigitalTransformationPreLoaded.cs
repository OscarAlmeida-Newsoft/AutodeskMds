using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class NS_TblDigitalTransformationPreLoaded
    {
        public int Id { get; set; }
        public int ProcessPreLoadedId { get; set; }
        public int TranslatorAdministratorPillarId { get; set; }
        public int TranslatorAdministratorTechnologyId { get; set; }
        public int TranslatorAdministratorBrandId { get; set; }
        public int TranslatorAdministratorCommentId { get; set; }
        public int? TranslatorAdministratorSolutionId { get; set; }

        public int Priority { get; set; }
        public bool Exist { get; set; }

        public virtual NS_TblProcessPreLoaded ProcessPreLoaded { get; set; }
    }
}
