using AutoMapper;
using DTOs;
using Entities;
using IRepositories;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository activityRepository;


        public ActivityService(IActivityRepository pActivityRepository)
        {
            activityRepository = pActivityRepository;
        }

        public ActivityDTO GetActivityById(int pActivityId)
        {
            
            var result = activityRepository.GetActivityById(pActivityId);

            return Mapper.Map<NS_TblActivity, ActivityDTO>(result);

        }

        public IEnumerable<ActivityDTO> GetAllActivities()
        {
            IEnumerable<ActivityDTO> _data = null;

            var results = activityRepository.GetAllActivities();

            if (results.Count() > 0)
            {
                _data = Mapper.Map<IEnumerable<NS_TblActivity>, IEnumerable<ActivityDTO>>(results);
            }

            return _data;
        }
    }
}
