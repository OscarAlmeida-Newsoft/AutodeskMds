using Affidavit.Models;
using Affidavit.Models.AdminMDS;
using Affidavit.Models.ManagePlatform;
using Affidavit.Utils;
using DTOs;
using DTOs.Landing;
using DTOs.SurveyQuestionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMapper
{
    public class DTOToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DTOToViewModelMappingProfile";
            }
        }

        public DTOToViewModelMappingProfile()
        {

            CreateMap<ProductFamilyDTO, FamilyViewModel>()
                //.ForMember(dest => dest.ProductFamilyID, options => options.MapFrom(src => src.ProductFamilyID))
                //.ForMember(dest => dest.ProductFamilyName, options => options.MapFrom(src => src.ProductFamilyName))
                .ForMember(dest => dest.Families, options => options.MapFrom(src => src.Products));

            CreateMap<ProductDTO, MicrosoftProductsViewModel>();

            CreateMap<PartnerCapabilityDTO, CompetenciasViewModel>();

            CreateMap<PartnerProgramDTO, ProgramasViewModel>();

            CreateMap<CompanyDTO, CompanySearchViewModel>();

            CreateMap<CRMDataDTO, CompanySearchViewModel>();

            //Mapeo clases que vienen SomSight
            CreateMap<SurveyQuestionResponseDTO, SurveyQuestionResponseVM>();

            CreateMap<LandingCampaignDTO, LandingsPerCountryVM>();


            CreateMap<AssessmentSummaryDTO, AssessmentGridViewModelDetail>()
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
                        ))

                ;


            CreateMap<AssessmentQuestionDTO, AssessmentQuestionsViewModel>()
                .ForMember(dest => dest.Options, options => options.MapFrom(src => src.AssessmentQuestionOptions))
                ;

            CreateMap<AssessmentQuestionOptionsDTO, AssessmentQuestionsOptionsViewModel>()
                ;

            CreateMap<AssessmentSummaryDTO, AssessmentSummaryVM>()
                .ForMember(dest => dest.DownloadPDF, options => options.MapFrom(src =>
                            src.ResponseEndDate != null ? true : false
                        ))
                .ForMember(dest => dest.IsInProgress, options => options.MapFrom(src =>
                            src.ResponseStartDate != null && src.ResponseEndDate == null ? true : false
            ));

            //Industry Insights
            CreateMap<IndustryIndInsDTO, IndustryVM>();
            CreateMap<ActivityDTO, ActivitiesVM>();
            CreateMap<DigitalTransformationDTO, DigitalTransformationVM>();
            CreateMap<ProcessDTO, CreateProcessVM>();
            CreateMap<ProblemDTO, ProblemVM>();
            CreateMap<IndustryInsightsAdminDTO, AdminIndustryInsightsVM>();
        }
    }
}