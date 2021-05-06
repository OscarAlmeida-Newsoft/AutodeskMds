namespace AutoMapper
{
    using Entities;
    using DTOs;
    using Entities.Landing;
    using DTOs.Landing;
    using Entities.SurveyQuestionResponse;
    using DTOs.SurveyQuestionResponse;
    using Entities.Recommendations;
    using Affidavit.Utils;

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
            CreateMap<NS_tblProduct, ProductDTO>()
                .ForMember(dest => dest.ProductID, options => options.MapFrom(src => src.ProductID));
                //.ForMember(dest => dest.ProductFamily, options => options.MapFrom(src => src.ProductFamily));

            CreateMap<NS_tblProductFamily, ProductFamilyDTO>()
                .ForMember(dest => dest.ProductFamilyID, options => options.MapFrom(src => src.ProductFamilyID))
                .ForMember(dest => dest.ProductFamilyName, options => options.MapFrom(src => src.ProductFamilyName))
                .ForMember(dest => dest.Products, options => options.MapFrom(src => src.NS_tblProduct));

            CreateMap<NS_tblIndustry, IndustryDTO>();

            CreateMap<NS_tblPartnerCapability, PartnerCapabilityDTO>();

            CreateMap<NS_tblPartnerProgram, PartnerProgramDTO>();

            CreateMap<NS_tblProductCompany, ProductCompanyDTO>();

            CreateMap<NS_tblQuestionxLanguage, QuestionxLanguageDTO>();

            CreateMap<NS_tblQuestion, QuestionDTO>();

            //CreateMap<NS_tblQuestionxProductFamily, QuestionxProductFamilyDTO>();

            //CreateMap<NS_tblResponseDataType, ResponseDataTypeDTO>();

            CreateMap<NS_tblResponseDataType, ResponseDataTypeDTO>();

            CreateMap<NS_tblLanguage, LanguageDTO>();

            CreateMap<NS_tblQuestionxProductFamily, QuestionxProductFamilyDTO>();

            CreateMap<NS_tblCompanyInfo, CompanyInfoDTO>();

            CreateMap<NS_tblCompany, CompanyDTO>();

            CreateMap<NS_tblCompanyContacts, CompanyContactsDTO>();

            CreateMap<NS_tblPartnerCapabilityCompany, PartnerCapabilityCompanyDTO>();

            CreateMap<NS_tblPartnerProgramCompany, PartnerProgramCompanyDTO>();

            CreateMap<NS_tblProductFamilyCompany, ProductFamilyCompanyDTO>();

            CreateMap<NS_tblAnswerCompany, AnswerCompanyDTO>()
                .ForMember(dest => dest.FamilyComplete, options => options.MapFrom(src => src.Product.ProductFamily.ProductFamilyName + " " + src.Product.ProductVersionGroup))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.Product.ProductName));

            CreateMap<NS_tblCampaign, CampaignDTO>();

            CreateMap<NS_tblCountry, CountryDTO>();

            CreateMap<NS_tblQuestion, QuestionDTO>();

            CreateMap<NS_tblProductFamily, ProductFamilyDTO>();

            //Mapeo de clases que vienen de SomSight
            CreateMap<LeadInformation, LeadInformationDTO>();

            CreateMap<LandingCampaign, LandingCampaignDTO>()
                .ForMember(dest => dest.CountryName, options => options.MapFrom(src => src.Country.CountryName))
                .ForMember(dest => dest.LanguageID, options => options.MapFrom(src => src.Country.LanguageID))
                ;

            CreateMap<SurveyQuestionResponse, SurveyQuestionResponseDTO>();


            //Assessments
            CreateMap<NS_TblAssessmentSummary, AssessmentSummaryDTO>()
                //.ForMember(dest => dest.AssessmentTypeDescription, options =>
                //    options.MapFrom(src => TranslatorHelper.TranslateTextById(src.AssessmentType.TranslatorAdministratorId)))
                //.ForMember(dest => dest.GlobalMaturityLeveDescription, options =>
                //    options.MapFrom(src => TranslatorHelper.TranslateTextById(
                //        src.GlobalMaturityLevel == null ? 0 : src.GlobalMaturityLevel.TranslatorAdministratorId
                //        )))
                ;
            CreateMap<NS_tblProductCompanyFile, ProductCompanyFileDTO>();

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

            //Para recomendaciones
            CreateMap<NS_tblVariable,VariableDTO>();
            CreateMap<NS_tblVariableProduct,VariableProductDTO>();
            CreateMap<NS_tblVariableProductFamily,VariableProductFamilyDTO>();
            CreateMap<NS_tblCompoundVariable, CompoundVariableDTO>();


            //Para Industry Insights
            CreateMap<NS_tblIndustryIndIns, IndustryIndInsDTO>();
            CreateMap<NS_TblActivity, ActivityDTO>();
            CreateMap<NS_TblProcessPreLoaded, ProcessDTO>();
            CreateMap<NS_TblProcess, ProcessDTO>();
            CreateMap<NS_TblProblemPreLoaded, ProblemDTO>();
            CreateMap<NS_TblProblem, ProblemDTO>();
            CreateMap<NS_TblDigitalTransformationPreLoaded, DigitalTransformationDTO>();
            CreateMap<NS_TblDigitalTransformation, DigitalTransformationDTO>();
        }

    }
}