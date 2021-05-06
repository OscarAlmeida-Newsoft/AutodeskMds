namespace AutoMapper
{
    using Entities;
    using DTOs;
    using DTOs.Landing;
    using Entities.Landing;
    using Entities.SurveyQuestionResponse;
    using DTOs.SurveyQuestionResponse;
    using DTOs.EmailQueueDTO;
    using Entities.NS_tblEmailQueue;
    using Entities.Recommendations;

    public class DTOToEntitiesMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DTOToEntitiesMappingProfile";
            }
        }

        public DTOToEntitiesMappingProfile()
        {
            CreateMap<ProductDTO, NS_tblProduct>()
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.ProductVersion, options => options.MapFrom(src => src.ProductVersion))
                .ForMember(dest => dest.ProductFamilyID, options => options.MapFrom(src => src.ProductFamilyID));

            CreateMap<PartnerCapabilityCompanyDTO, NS_tblPartnerCapabilityCompany>();

            CreateMap<PartnerProgramCompanyDTO, NS_tblPartnerProgramCompany>();

            CreateMap<ProductCompanyDTO, NS_tblProductCompany>();

            CreateMap<ProductFamilyCompanyDTO, NS_tblProductFamilyCompany>();

            CreateMap<CompanyDTO, NS_tblCompany>();

            CreateMap<CampaignDTO, NS_tblCampaign>();

            CreateMap<CompanyContactsDTO, NS_tblCompanyContacts>();

            CreateMap<CompanyInfoDTO, NS_tblCompanyInfo>();

            CreateMap<AnswerCompanyDTO, NS_tblAnswerCompany>();

            CreateMap<ResponseDataTypeDTO, NS_tblResponseDataType>();

            CreateMap<QuestionDTO, NS_tblQuestion>()
                .ForMember(dest => dest.ResponseDataType, options => options.MapFrom(src => src.ResponseDataType))
                .ForMember(dest => dest.ReletedQuestionID, options => options.MapFrom(src => src.ReletedQuestionID))
                .ForMember(dest => dest.RelatedQuestionIDResponse, options => options.MapFrom(src => src.RelatedQuestionIDResponse));

            CreateMap<QuestionxLanguageDTO, NS_tblQuestionxLanguage>();

            CreateMap<QuestionxProductFamilyDTO, NS_tblQuestionxProductFamily>();

            CreateMap<ProductFamilyDTO, NS_tblProductFamily>();

            //Mapeo clases que vienen SomSight
            CreateMap<LeadInformationDTO, LeadInformation>();

            CreateMap<LandingCampaignDTO, LandingCampaign>();

            CreateMap<SurveyQuestionResponseDTO, SurveyQuestionResponse>();

            CreateMap<EmailQueueDTO, NS_tblEmailQueue>();

            CreateMap<ProductCompanyFileDTO, NS_tblProductCompanyFile>();

            //Para recomendaciones
            CreateMap<VariableDTO, NS_tblVariable>();
            CreateMap<VariableProductDTO, NS_tblVariableProduct>();
            CreateMap<VariableProductFamilyDTO, NS_tblVariableProductFamily>();
            CreateMap<CompoundVariableDTO, NS_tblCompoundVariable>();

            //Industry Insights
            CreateMap<ProcessDTO, NS_TblProcess>();
            CreateMap<ProblemDTO, NS_TblProblem>();
            CreateMap<DigitalTransformationDTO, NS_TblDigitalTransformation>();
        }
    }
}