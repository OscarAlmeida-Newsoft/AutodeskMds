using System;
using IRepositories;
using IServices;
using System.Collections.Generic;
using DTOs;
using DTOs.Utils;

namespace Services
{
    public class TranslatorService : ITranslatorService
    {
        private ITranslationXLanguageRepository translationRepository;
        private ITranslatorUtility translatorUtility;

        public TranslatorService(ITranslationXLanguageRepository pTranslationRepository, ITranslatorUtility pTranslatorUtility)
        {
            translationRepository = pTranslationRepository;
            translatorUtility = pTranslatorUtility;
        }

        public void SaveTranslationFileObject(int pLanguageId)
        {
            TranslatorExternalService.TranslatorServicesClient _client = new TranslatorExternalService.TranslatorServicesClient();
            IEnumerable<TranslatorExternalService.TranslatorFileModel> _fileModel = _client.TranslatorFileModel(pLanguageId);
            List<TranslatorTextModelDTO> _fileModelDTO = new List<TranslatorTextModelDTO>();


            foreach (var item in _fileModel)
            {
                _fileModelDTO.Add(new TranslatorTextModelDTO
                {
                    Id = item.Id,
                    ApplyLeadsTemplate = item.ApplyLeadsTemplate,
                    TextIdentifier = item.TextIdentifier,
                    IsForDeveloperUse = item.IsForDeveloperUse,
                    TranslatedText = item.TranslatedText
                });
            }
            
            translatorUtility.SaveJsonLanguageFile(pLanguageId, _fileModelDTO);

        }
    }
}
