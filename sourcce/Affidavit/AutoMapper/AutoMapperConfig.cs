namespace AutoMapper
{
    using Affidavit.AutoMapper;
    using AutoMapper;
    public class AutoMapperConfig
    {
       public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<EntitiesToDTOMappingProfile>();
                x.AddProfile<ViewModelToDtoMappingProfile>();
                x.AddProfile<DTOToEntitiesMappingProfile>();
                x.AddProfile<DTOToViewModelMappingProfile>();
                
            });
        }
    }
}