using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOMSight.Automapper
{
    public class AutoMapperConfig
    {

        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<EntitiesToDTOMappingProfile>();
                x.AddProfile<DomainToDtoMappingProfile>();
                x.AddProfile<ViewModelToDtoMappingProfile>();
                x.AddProfile<DtoToDomainMappingProfile>();
                x.AddProfile<DtoToViewModelMappingProfile>();
            });
        }
    }
}