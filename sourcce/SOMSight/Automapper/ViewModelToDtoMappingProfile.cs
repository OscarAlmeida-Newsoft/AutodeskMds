using AutoMapper;
using SOMSight.Models;
using SOMSightModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOMSight.Automapper
{
    public class ViewModelToDtoMappingProfile : Profile
    {

        public override string ProfileName
        {
            get { return "ViewModelToDtoMapping"; }
        }

        public ViewModelToDtoMappingProfile()
        {
            CreateMap<AssessmentQuestionAnswerDetailsViewModel, AssessmentQuestionAnswerDetailsDTO>();
        }

        
    }
}