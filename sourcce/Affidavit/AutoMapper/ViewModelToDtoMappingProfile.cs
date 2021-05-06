using Affidavit.Models;
using Affidavit.Models.AdminMDS;
using Affidavit.Models.ManagePlatform;
using AutoMapper;
using DTOs;
using DTOs.Landing;
using DTOs.SurveyQuestionResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Affidavit.AutoMapper
{
    public class ViewModelToDtoMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDtoMapping"; }
        }

        public ViewModelToDtoMappingProfile()
        {
            CreateMap<SaveLandingVM, LandingCampaignDTO>()
            .ForMember(dest => dest.Id, options => options.MapFrom(src => src.LandingId))
            .ForMember(dest => dest.Campaing, options => options.MapFrom(src => src.LandingCampaign))
            .ForMember(dest => dest.LandingText, options => options.MapFrom(src => src.LandingText))
            ;

            CreateMap<ReplaceMultipleLandingVM, ReplaceMultipleLandingDTO>();

            CreateMap<DisagreeReasonVM, DisagreeReasonDTO>();

            CreateMap<SurveyQuestionResponseVM, SurveyQuestionResponseDTO>();

            CreateMap<LicensesTypesViewModel, ProductCompanyDTO>()
                .ForMember(dest => dest.ProductID, options => options.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.InstalledLicenses, options => options.MapFrom(src => src.InstalledLicenses))
                .ForMember(dest => dest.VLS, options => options.MapFrom(src => src.VLS))
                .ForMember(dest => dest.OEM, options => options.MapFrom(src => src.OEM))
                .ForMember(dest => dest.FPP, options => options.MapFrom(src => src.FPP))
                .ForMember(dest => dest.WEB, options => options.MapFrom(src => src.WEB))
                .ForMember(dest => dest.CompanyID, options => options.MapFrom(src => src.CompanyID))
                .ForMember(dest => dest.CampaignID, options => options.MapFrom(src => src.CampaignID));

            CreateMap<AssessmentQuestionAnswerDetailsViewModel, AssessmentQuestionAnswerDetailsDTO>();

            //Industry Insights
            CreateMap<CreateProcessVM, ProcessDTO>();
            CreateMap<DigitalTransformationVM, DigitalTransformationDTO>();
            CreateMap<ProblemVM, ProblemDTO>();
        }
    }
}