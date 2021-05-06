using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IActivityRepository
    {
        IEnumerable<NS_TblActivity> GetAllActivities();
        NS_TblActivity GetActivityById(int pActivityId);
    }
}
