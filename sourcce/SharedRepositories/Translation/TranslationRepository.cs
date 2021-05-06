using ISharedRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SharedRepositories
{
    public class TranslationRepository : ITranslationRepository
    {
        public ProcessResult CreateTranslatorLanguage(NS_tblTranslatorLanguage pModel)
        {
            ProcessResult _results = new ProcessResult();

            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            try
            {
                Database _db = _factory.Create("SharedContext");
                int _num = _db.ExecuteNonQuery("NS_spCreateTranslatorLanguage", pModel.Name, pModel.Code, pModel.FlagPath, pModel.CreatedById);

                _results.ProcessResults = true;
                
            }
            catch (Exception ex)
            {

                _results.ProcessResults = false;
                _results.Message = ex.Message;
                _results.MessageDetails = ex.InnerException?.Message;
            }

            return _results;
        }


        public ProcessResult UpdateTranslatorLanguage(NS_tblTranslatorLanguage pModel)
        {
            ProcessResult _results = new ProcessResult();

            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            try
            {
                Database _db = _factory.Create("SharedContext");
                int _num = _db.ExecuteNonQuery("NS_spUpdateTranslatorLanguage", pModel.Id, pModel.Name, pModel.FlagPath, pModel.UpdatedById);

                _results.ProcessResults = true;

            }
            catch (Exception ex)
            {

                _results.ProcessResults = false;
                _results.Message = ex.Message;
                _results.MessageDetails = ex.InnerException?.Message;
            }

            return _results;
        }


        public ProcessResult DeleteTranslatorLanguage(NS_tblTranslatorLanguage pModel)
        {
            ProcessResult _results = new ProcessResult();

            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            try
            {
                Database _db = _factory.Create("SharedContext");
                int _num = _db.ExecuteNonQuery("NS_spDeleteTranslatorLanguage", pModel.Id, pModel.UpdatedById);

                _results.ProcessResults = true;

            }
            catch (Exception ex)
            {

                _results.ProcessResults = false;
                _results.Message = ex.Message;
                _results.MessageDetails = ex.InnerException?.Message;
            }

            return _results;
        }

        public IEnumerable<NS_tblTranslatorLanguageDropDown> GetTranslatorLanguageDropDown()
        {
            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            IRowMapper<NS_tblTranslatorLanguageDropDown> _rowMapper = MapBuilder<NS_tblTranslatorLanguageDropDown>.BuildAllProperties();
            IEnumerable<NS_tblTranslatorLanguageDropDown> _data = null;

            try
            {
                Database _db = _factory.Create("SharedContext");
                _data = _db.ExecuteSprocAccessor<NS_tblTranslatorLanguageDropDown>("NS_spReadTranslatorLanguageDropDown", _rowMapper);
            }
            catch (Exception)
            {

                throw;
            }

            return _data;
        }



        public IEnumerable<TranslatorFileModel> GetTranslationFileModel(int pLanguageId)
        {
            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            IRowMapper<TranslatorFileModel> _rowMapper = MapBuilder<TranslatorFileModel>.BuildAllProperties();
            IEnumerable<TranslatorFileModel> _data = null;

            try
            {
                Database _db = _factory.Create("SharedContext");
                _data = _db.ExecuteSprocAccessor<TranslatorFileModel>("NS_spGetTranslationFileData", _rowMapper, pLanguageId);
            }
            catch (Exception)
            {

                throw;
            }

            return _data;
        }

        public ProcessResult CreateTranslatorAdministrator(NS_TblTranslatorAdministrator pModel)
        {
            ProcessResult _results = new ProcessResult();

            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            try
            {
                Database _db = _factory.Create("SharedContext");
                int _num = _db.ExecuteNonQuery("NS_spCreateTranslatorAdministrator", pModel.TextIdentifier, pModel.Description
                    , pModel.DefaultTextValue, pModel.ApplyLeadsTemplate, pModel.IsForDeveloperUse, pModel.CreatedById);

                _results.ProcessResults = true;

            }
            catch (Exception ex)
            {

                _results.ProcessResults = false;
                _results.Message = ex.Message;
                _results.MessageDetails = ex.InnerException?.Message;
            }

            return _results;
        }

        public ProcessResult UpdateTranslatorAdministrator(NS_TblTranslatorAdministrator pModel)
        {
            ProcessResult _results = new ProcessResult();

            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            try
            {
                Database _db = _factory.Create("SharedContext");
                int _num = _db.ExecuteNonQuery("NS_spUpdateTranslatorAdministrator", pModel.Id, pModel.Description, pModel.DefaultTextValue
                    , pModel.ApplyLeadsTemplate, pModel.IsForDeveloperUse, pModel.UpdatedById);

                _results.ProcessResults = true;

            }
            catch (Exception ex)
            {

                _results.ProcessResults = false;
                _results.Message = ex.Message;
                _results.MessageDetails = ex.InnerException?.Message;
            }

            return _results;
        }

        public ProcessResult DeleteTranslatorAdministrator(NS_TblTranslatorAdministrator pModel)
        {
            ProcessResult _results = new ProcessResult();

            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            try
            {
                Database _db = _factory.Create("SharedContext");
                int _num = _db.ExecuteNonQuery("NS_spDeleteTranslatorAdministrator", pModel.Id);

                _results.ProcessResults = true;

            }
            catch (Exception ex)
            {

                _results.ProcessResults = false;
                _results.Message = ex.Message;
                _results.MessageDetails = ex.InnerException?.Message;
            }

            return _results;
        }

        public IEnumerable<NS_TblTranslatorAdministrator> GetTranslatorAdministrator()
        {
            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            IRowMapper<NS_TblTranslatorAdministrator> _rowMapper = MapBuilder<NS_TblTranslatorAdministrator>.BuildAllProperties();
            IEnumerable<NS_TblTranslatorAdministrator> _data = null;

            try
            {
                Database _db = _factory.Create("SharedContext");
                _data = _db.ExecuteSprocAccessor<NS_TblTranslatorAdministrator>("NS_spReadTranslatorAdministrator", _rowMapper);
            }
            catch (Exception)
            {

                throw;
            }

            return _data;
        }

        public ProcessResult CreateTranslatorXLanguage(NS_TblTranslationXLanguage pModel)
        {
            ProcessResult _results = new ProcessResult();

            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            try
            {
                Database _db = _factory.Create("SharedContext");
                int _num = _db.ExecuteNonQuery("NS_spCreateTranslatorXLanguage", pModel.TranslationAdministrationId, pModel.LanguageId, pModel.TranslationText, pModel.CreatedById);

                _results.ProcessResults = true;

            }
            catch (Exception ex)
            {

                _results.ProcessResults = false;
                _results.Message = ex.Message;
                _results.MessageDetails = ex.InnerException?.Message;
            }

            return _results;
        }

        public ProcessResult UpdateTranslatorXLanguage(NS_TblTranslationXLanguage pModel)
        {
            ProcessResult _results = new ProcessResult();

            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            try
            {
                Database _db = _factory.Create("SharedContext");
                int _num = _db.ExecuteNonQuery("NS_spUpdateTranslatorXLanguage", pModel.Id, pModel.TranslationText, pModel.UpdatedById);

                _results.ProcessResults = true;

            }
            catch (Exception ex)
            {

                _results.ProcessResults = false;
                _results.Message = ex.Message;
                _results.MessageDetails = ex.InnerException?.Message;
            }

            return _results;
        }

        public ProcessResult DeleteTranslatorXLanguage(NS_TblTranslationXLanguage pModel)
        {
            ProcessResult _results = new ProcessResult();

            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            try
            {
                Database _db = _factory.Create("SharedContext");
                int _num = _db.ExecuteNonQuery("NS_spDeleteTranslatorXLanguage", pModel.Id);

                _results.ProcessResults = true;

            }
            catch (Exception ex)
            {

                _results.ProcessResults = false;
                _results.Message = ex.Message;
                _results.MessageDetails = ex.InnerException?.Message;
            }

            return _results;
        }

        public IEnumerable<NS_TblTranslationXLanguage> GetTranslationXLanguage()
        {
            DatabaseProviderFactory _factory = new DatabaseProviderFactory();
            IRowMapper<NS_TblTranslationXLanguage> _rowMapper = MapBuilder<NS_TblTranslationXLanguage>.BuildAllProperties();
            IEnumerable<NS_TblTranslationXLanguage> _data = null;

            try
            {
                Database _db = _factory.Create("SharedContext");
                _data = _db.ExecuteSprocAccessor<NS_TblTranslationXLanguage>("NS_spReadTranslatorXLanguage", _rowMapper);
            }
            catch (Exception)
            {

                throw;
            }

            return _data;
        }
    }
}
