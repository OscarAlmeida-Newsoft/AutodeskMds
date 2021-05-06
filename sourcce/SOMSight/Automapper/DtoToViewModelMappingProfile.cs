using AutoMapper;
using SOMSight.Models;
using SOMSight.Models.Account;
using SOMSight.Utils;
using SOMSightModels.DTOs;
using SOMSightModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOMSight.Automapper
{
    public class DtoToViewModelMappingProfile : Profile
    {

        public override string ProfileName
        {
            get { return "DtoToViewModelMappingProfile"; }
        }

        public DtoToViewModelMappingProfile()
        {
            #region User

            CreateMap<UserDTO, UserSession>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserEmail, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, options => options.MapFrom(src => src.UserName))
                ;
            #endregion

            CreateMap<AssessmentSummaryDTO, AssessmentGridViewModel>()
                .ForMember(dest => dest.AssessmentSummaryId, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.AssessmentTypeId, options => options.MapFrom(src => src.AssessmentTypeId))
                //.ForMember(dest => dest.AssessmentName, options => options.MapFrom(src => src.AssessmentTypeDescription))
                .ForMember(dest => dest.AssessmentStatus, options => options.MapFrom(src =>
                            src.ResponseEndDate != null ? TranslatorHelper.TranslateTextByIdentifier("assessment_status_completed") :
                            src.ResponseStartDate != null ? TranslatorHelper.TranslateTextByIdentifier("assessment_status_in_progress") :
                            TranslatorHelper.TranslateTextByIdentifier("assessment_status_to_do")
                        ))
                .ForMember(dest => dest.IconName, options => options.MapFrom(src => src.IconName))
                .ForMember(dest => dest.IsMDSAssessment, options => options.MapFrom(src => false))
                .ForMember(dest => dest.DownloadPDF, options => options.MapFrom(src =>
                            src.ResponseEndDate != null ? true : false
                        ))
                .ForMember(dest => dest.IsInProgress, options => options.MapFrom(src =>
                            src.ResponseStartDate != null ? true : false
            ));

            CreateMap<AssessmentQuestionDTO, AssessmentQuestionsViewModel>()
                        .ForMember(dest => dest.Options, options => options.MapFrom(src => src.AssessmentQuestionOptions))
                        ;

            CreateMap<AssessmentQuestionOptionsDTO, AssessmentQuestionsOptionsViewModel>()
                ;

        }
    }
}