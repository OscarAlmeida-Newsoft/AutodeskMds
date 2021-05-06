using AutoMapper;
using SOMSightModels;
using SOMSightModels.DTOs;

namespace SOMSight.Automapper
{


    public class EntitiesToDTOMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "EntitiesToDTOMappingProfile";
            }
        }


        public EntitiesToDTOMappingProfile()
        {

            //Assessments
            CreateMap<NS_TblAssessmentSummary, AssessmentSummaryDTO>()
                //.ForMember(dest => dest.AssessmentTypeDescription, options =>
                //    options.MapFrom(src => TranslatorHelper.TranslateTextById(src.AssessmentType.TranslatorAdministratorId)))
                //.ForMember(dest => dest.GlobalMaturityLeveDescription, options =>
                //    options.MapFrom(src => TranslatorHelper.TranslateTextById(
                //        src.GlobalMaturityLevel == null ? 0 : src.GlobalMaturityLevel.TranslatorAdministratorId
                //        )))
                ;

            CreateMap<NS_TblAssessmentQuestion, AssessmentQuestionDTO>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.DisplayOrder, options => options.MapFrom(src => src.DisplayOrder))
                //.ForMember(dest => dest.AssessmentQuestionText, options =>
                //    //options.MapFrom(src => TranslatorHelper.TranslateTextById(src.TranslatorAdministratorId)))
                //    options.MapFrom(src => src.TranslatorAdministrator.TextIdentifier))
                //.ForMember(dest => dest.AssessmentQuestionOptions, options => options.MapFrom( src => src.AssessmentQuestionOptions))
                ;

            CreateMap<NS_TblAssessmentQuestionOption, AssessmentQuestionOptionsDTO>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.MaturityLevelId, options => options.MapFrom(src => src.MaturityLevelId))
                //.ForMember(dest => dest.OptionText, options =>
                //   // options.MapFrom(src => TranslatorHelper.TranslateTextById(src.TranslatorAdministratorId)))
                //    options.MapFrom(src => src.TranslatorAdministrator.TextIdentifier))
                ;


        }

    }
}