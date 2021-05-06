using AutoMapper;
using SOMSightModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOMSight.Automapper
{
    public class DomainToDtoMappingProfile : Profile
    {

        public override string ProfileName
        {
            get { return "DomainToDtoMappingProfile"; }
        }
        public DomainToDtoMappingProfile()
        {
            #region User
            CreateMap<User, UserDTO>()
                ;
            #endregion
        }
    }
}