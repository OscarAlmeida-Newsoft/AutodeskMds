using ISOMSightServices;
using SOMSightModels.DTOs;
using SOMSightModels.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOMSightServices
{
    public class TranslatorService : ITranslatorService
    {
        private ITranslatorUtility translatorUtility;

        public TranslatorService(ITranslatorUtility pTranslatorUtility)
        {
            translatorUtility = pTranslatorUtility;
        }


        public void SaveTranslationFileObject(int pLanguageId)
        {
            TranslatorExternalService.TranslatorServicesClient _client = new TranslatorExternalService.TranslatorServicesClient();
            IEnumerable<TranslatorExternalService.TranslatorFileModel> _fileModel = _client.TranslatorFileModel(pLanguageId);
            List<TranslatorFileModelDTO> _fileModelDTO = new List<TranslatorFileModelDTO>();


            foreach (var item in _fileModel)
            {
                _fileModelDTO.Add(new TranslatorFileModelDTO
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
