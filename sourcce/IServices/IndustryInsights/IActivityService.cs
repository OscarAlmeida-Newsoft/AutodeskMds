using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IActivityService
    {
        IEnumerable<ActivityDTO> GetAllActivities();

        ActivityDTO GetActivityById(int pActivityId);
    }
}
