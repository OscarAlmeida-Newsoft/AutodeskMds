using Entities;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class QuestionRepository : Repository<NS_tblQuestion>, IQuestionRepository
    {
        public QuestionRepository(AffidavitContext context) : base(context)
        {

        }

        public override IEnumerable<NS_tblQuestion> GetAll()
        {
            var data = Context.Set<NS_tblQuestion>().ToList();

            return data;
        }


    }
}
