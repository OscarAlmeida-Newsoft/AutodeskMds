using Affidavit.Helpers;
using Affidavit.Models;
using Affidavit.Models.Product;
using Affidavit.Models.Question;
using DTOs;
using Entities.DbAffidavit;
using IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace Affidavit.Controllers
{
    #if !DEBUG
    [RequireHttps] //apply to all actions in controller
    #endif
    public class QuestionController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageQuestion()
        {
            if (Session["userLogin"] == null)
            {
                return RedirectToAction("../Home/LoginAdmin");
            }

            string _currentCulture = CultureHelper.GetCurrentCulture();
            int _lenguageID = _currentCulture == "es" ? 2 : 1;

            ManageQuestionViewModel _model = new ManageQuestionViewModel();
            List<QuestionListViewModel> _questionList = new List<QuestionListViewModel>();

            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                _questionList = (from q in db.NS_spQuestion_SelectByLanguage()
                                 select new QuestionListViewModel
                                 {
                                     DisplayOrder = q.DisplayOrder,
                                     IsActive = (q.IsActive == 1) ? true : false,
                                     ProductFamilyID = q.ProductFamilyID,
                                     QuestionID = q.QuestionID,
                                     QuestionText = q.QuestionText,
                                     RelatedQuestionIDResponse = q.RelatedQuestionIDResponse,
                                     ReletedQuestionID = q.ReletedQuestionID,
                                     ResponseDataTypeID = q.ResponseDataTypeID,
                                     ProductFamilyName = q.ProductFamilyName,
                                     ResponseDataTypeDescription = q.ResponseDataTypeDescription
                                 }).ToList();
            }

            _model.QuestionList = _questionList;

            return View("ManageQuestion", _model);
        }

        /// <summary>
        /// carga el modal para editar una pregunta
        /// </summary>
        /// <param name="pQuestionID">id de la pregunta</param>
        /// <param name="pProductFamilyID">id de la familia del producto </param>
        /// <returns></returns>
        public ActionResult EditQuestion(int? pQuestionID, int pProductFamilyID)
        {
            string _currentCulture = CultureHelper.GetCurrentCulture();
            int _lenguageID = _currentCulture == "es" ? 2 : 1;

            List<ResponseDataTypeVIewModel> _resDataType = new List<ResponseDataTypeVIewModel>();
            List<LanguageViewModel> _languages = new List<LanguageViewModel>();
            List<ProductFamilyViewModel> _productFamily = new List<ProductFamilyViewModel>();
            List<QuestionxLanguageViewModel> _questionxLanguage = new List<QuestionxLanguageViewModel>();
            List<AnswerCompanyViewModel> _answerCompany = new List<AnswerCompanyViewModel>();
            List<QuestionxProductFamilyViewModel> _questionxProductFamily = new List<QuestionxProductFamilyViewModel>();
            QuestionViewModel _question = new QuestionViewModel();

            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                _resDataType = (from rdt in db.NS_spResponseDataType_Select()
                                select new ResponseDataTypeVIewModel
                                {
                                    ResponseDataTypeDescription = rdt.ResponseDataTypeDescription,
                                    ResponseDataTypeID = rdt.ResponseDataTypeID,
                                    ResponseDataTypeLength = rdt.ResponseDataTypeLength
                                }).ToList();

                _languages = (from l in db.NS_spLanguages_Select()
                              select new LanguageViewModel
                              {
                                  LanguageID = l.LanguageID,
                                  LanguageName = l.LanguageName
                              }).ToList();

                _productFamily = (from pf in db.NS_spProductFamily_SelectAll()
                                  select new ProductFamilyViewModel
                                  {
                                      ProductFamilyID = pf.ProductFamilyID,
                                      ProductFamilyName = pf.ProductFamilyName
                                  }).ToList();

                _questionxLanguage = (from ql in db.NS_spQuestionByLanguage_Select()
                                      select new QuestionxLanguageViewModel
                                      {
                                          LanguageID = ql.LanguageID,
                                          QuestionID = ql.QuestionID,
                                          QuestionText = ql.QuestionText
                                      }).ToList();

                _answerCompany = (from ac in db.NS_spAnswerCompany_SelectQuestionID()
                                  select new AnswerCompanyViewModel
                                  {
                                      QuestionID = ac.QuestionID
                                  }).ToList();

                _questionxProductFamily = (from qpf in db.NS_spQuestionxProductFamily_Select()
                                           select new QuestionxProductFamilyViewModel
                                           {
                                               DisplayOrder = qpf.DisplayOrder,
                                               IsActive = qpf.IsActive,
                                               QuestionID = qpf.QuestionID,
                                               ProductFamilyID = qpf.ProductFamilyID
                                           }).ToList();

                _question = (from q in db.NS_spQuestionByID_Select(pQuestionID)
                             select new QuestionViewModel
                             {
                                 QuestionID = q.QuestionID,
                                 RelatedQuestionIDResponse = q.RelatedQuestionIDResponse,
                                 ReletedQuestionID = q.ReletedQuestionID,
                                 ResponseDataTypeID = q.ResponseDataTypeID
                             }).FirstOrDefault();


            }

            List<QuestionListViewModel> _questionList = new List<QuestionListViewModel>();

            List<int> _answerCompanyList = _answerCompany.Select(id => id.QuestionID).ToList();

            QuestionxProductFamilyViewModel _questionxProductFamilyVM = _questionxProductFamily.Where(s => s.QuestionID == pQuestionID.Value && s.ProductFamilyID == pProductFamilyID).FirstOrDefault();

            List<SelectListItem> _responseDataType = new List<SelectListItem>();

            IEnumerable<QuestionxLanguageViewModel> _preguntasxLanguageList = _questionxLanguage.Where(p => p.LanguageID == 1);

            CreateNewQuestionViewModel _newQuestion = new CreateNewQuestionViewModel();

            foreach (var item in _resDataType)
            {
                _responseDataType.Add(new SelectListItem
                {
                    Text = item.ResponseDataTypeDescription,
                    Value = item.ResponseDataTypeID.ToString(),
                });
            }

            _newQuestion.Languages = _languages;
            _newQuestion.QuestionsxLanguages = _questionxLanguage;
            _newQuestion.ResponseDataTypeListItem = _responseDataType;
            _newQuestion.ProductFamilyList = new SelectList(_productFamily, "ProductFamilyID", "ProductFamilyName");
            _newQuestion.RelatedQuestionList = new SelectList(_preguntasxLanguageList, "QuestionID", "QuestionText", _question.ReletedQuestionID);
            _newQuestion.RelatedQuestionResponseList = new SelectList(_resDataType, "ResponseDataTypeID", "ResponseDataTypeDescription");
            _newQuestion.CurrentQuestionID = pQuestionID.Value;
            _newQuestion.Order = _questionxProductFamilyVM.DisplayOrder;
            _newQuestion.ProductFamilyID = _questionxProductFamilyVM.ProductFamilyID;
            _newQuestion.RelatedQuestionIDResponse = _question.RelatedQuestionIDResponse;
            _newQuestion.ResponseDataTypeID = _question.ResponseDataTypeID;
            _newQuestion.LanguagesQuestionsText = _questionxLanguage.Where(s => s.QuestionID == pQuestionID.Value).Select(s => s.QuestionText).ToList();
            _newQuestion.ResponseDataTypeList = new SelectList(_resDataType, "ResponseDataTypeID", "ResponseDataTypeDescription", _newQuestion.ResponseDataTypeID);
            switch (_questionxProductFamilyVM.IsActive)
            {
                case (int)IsActiveFlag.False:
                    _newQuestion.ActiveFlag = IsActiveFlag.False;
                    break;
                case (int)IsActiveFlag.True:
                    _newQuestion.ActiveFlag = IsActiveFlag.True;
                    break;
            }

            _newQuestion.ExisteID = (_answerCompanyList.Contains(pQuestionID.Value)) ? true : false;

            return PartialView("_EditQuestionPartial", _newQuestion);
        }

        /// <summary>
        /// Actualiza un registro de la tabla
        /// </summary>
        /// <param name="pUpdateQuestion"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateQuestion(CreateNewQuestionViewModel pUpdateQuestion)
        {
            string _currentCulture = CultureHelper.GetCurrentCulture();
            int _lenguageID = _currentCulture == "es" ? 2 : 1;
            int? _reletedQuestionID = 0;
            var _questionID = 0;

            if (pUpdateQuestion.RelatedQuestionID != 0)
            {
                _reletedQuestionID = pUpdateQuestion.RelatedQuestionID;
            }

            _questionID = pUpdateQuestion.CurrentQuestionID;

            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                using (var _transac = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.NS_spQuestion_Update(_questionID, pUpdateQuestion.ResponseDataTypeID,
                        (_reletedQuestionID != 0) ? _reletedQuestionID : null, pUpdateQuestion.RelatedQuestionResponseID.ToString());

                        for (var i = 0; i < pUpdateQuestion.LanguagesID.Count(); i++)
                        {
                            db.NS_spQuestionxLanguage_Update(_questionID, pUpdateQuestion.LanguagesID[i],
                                pUpdateQuestion.LanguagesQuestionsText[i]);
                        }

                        db.NS_spQuestionxProductFamily_Update((byte)pUpdateQuestion.ProductFamilyID, pUpdateQuestion.CurrentQuestionID,
                            pUpdateQuestion.Order, (pUpdateQuestion.ActiveFlag == IsActiveFlag.True) ? 1 : 0);

                        db.SaveChanges();
                        _transac.Commit();
                    }
                    catch (Exception)
                    {
                        _transac.Rollback();
                    }
                }

            }

            return RedirectToAction("ManageQuestion");
        }

        /// <summary>
        ///  Action para mostrar modal para asignar un nuevo producto a una pregunta
        /// </summary>
        /// <param name="pQuestionID">Id de la pregunta</param>
        /// <returns></returns>
        public ActionResult AssiNewProductToQuestion(int pQuestionID)
        {
            string _currentCulture = CultureHelper.GetCurrentCulture();
            int _lenguageID = _currentCulture == "es" ? 2 : 1;

            List<ProductFamilyViewModel> _productFamily = new List<ProductFamilyViewModel>();
            List<QuestionxProductFamilyViewModel> _questionxProductFamily = new List<QuestionxProductFamilyViewModel>();

            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                _productFamily = (from pf in db.NS_spProductFamily_SelectAll()
                                  select new ProductFamilyViewModel
                                  {
                                      ProductFamilyID = pf.ProductFamilyID,
                                      ProductFamilyName = pf.ProductFamilyName
                                  }).ToList();

                _questionxProductFamily = (from qpf in db.NS_spQuestionxProductFamily_Select()
                                           select new QuestionxProductFamilyViewModel
                                           {
                                               DisplayOrder = qpf.DisplayOrder,
                                               IsActive = qpf.IsActive,
                                               QuestionID = qpf.QuestionID,
                                               ProductFamilyID = qpf.ProductFamilyID
                                           }).ToList();
            }

            List<byte> _questionxProductFamilyID = _questionxProductFamily.Where(s => s.QuestionID == pQuestionID).Select(s => s.ProductFamilyID).ToList();

            IEnumerable<ProductFamilyViewModel> _productFamilyShort = _productFamily.Where(s => !_questionxProductFamilyID.Contains((byte)s.ProductFamilyID));

            AssignProductToQuestionViewModel _assignProductToQuestion = new AssignProductToQuestionViewModel();

            _assignProductToQuestion.ProductFamilyList = new SelectList(_productFamilyShort, "ProductFamilyID", "ProductFamilyName");
            _assignProductToQuestion.QuestionID = pQuestionID;

            //_newQuestion.Order = _questionxProductFamily.DisplayOrder;
            //_newQuestion.ProductFamilyID = _questionxProductFamily.ProductFamilyID;
            return PartialView("_AssiNewProductToQuestionPartial", _assignProductToQuestion);
        }

        /// <summary>
        ///  Action para mostrar modal para asignar un nuevo producto a una pregunta
        /// </summary>
        /// <param name="pQuestionID">Id de la pregunta</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AssiNewProductToQuestion(AssignProductToQuestionViewModel pAssignProductToQuestion)
        {
            string _currentCulture = CultureHelper.GetCurrentCulture();
            int _lenguageID = _currentCulture == "es" ? 2 : 1;

            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                db.NS_spQuestionxProductFamily_Insert(pAssignProductToQuestion.QuestionID, pAssignProductToQuestion.ProductFamilyID,
                   pAssignProductToQuestion.DisplayOrder);
            }

            return RedirectToAction("ManageQuestion");
        }

        /// <summary>
        ///     carga el formulario para crear una nueva pregunta.
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateNewQuestion()
        {
            string _currentCulture = CultureHelper.GetCurrentCulture();
            int _lenguageID = _currentCulture == "es" ? 2 : 1;

            List<ResponseDataTypeVIewModel> _resDataType = new List<ResponseDataTypeVIewModel>();
            List<LanguageViewModel> _languages = new List<LanguageViewModel>();
            List<ProductFamilyViewModel> _productFamily = new List<ProductFamilyViewModel>();
            List<QuestionxLanguageViewModel> _questionxLanguage = new List<QuestionxLanguageViewModel>();
            List<AnswerCompanyViewModel> _answerCompany = new List<AnswerCompanyViewModel>();
            List<QuestionxProductFamilyViewModel> _questionxProductFamily = new List<QuestionxProductFamilyViewModel>();

            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                _resDataType = (from rdt in db.NS_spResponseDataType_Select()
                                select new ResponseDataTypeVIewModel
                                {
                                    ResponseDataTypeDescription = rdt.ResponseDataTypeDescription,
                                    ResponseDataTypeID = rdt.ResponseDataTypeID,
                                    ResponseDataTypeLength = rdt.ResponseDataTypeLength
                                }).ToList();

                _languages = (from l in db.NS_spLanguages_Select()
                              select new LanguageViewModel
                              {
                                  LanguageID = l.LanguageID,
                                  LanguageName = l.LanguageName
                              }).ToList();

                _productFamily = (from pf in db.NS_spProductFamily_SelectAll()
                                  select new ProductFamilyViewModel
                                  {
                                      ProductFamilyID = pf.ProductFamilyID,
                                      ProductFamilyName = pf.ProductFamilyName
                                  }).ToList();

                _questionxLanguage = (from ql in db.NS_spQuestionByLanguage_Select()
                                      select new QuestionxLanguageViewModel
                                      {
                                          LanguageID = ql.LanguageID,
                                          QuestionID = ql.QuestionID,
                                          QuestionText = ql.QuestionText
                                      }).ToList();

                _answerCompany = (from ac in db.NS_spAnswerCompany_SelectQuestionID()
                                  select new AnswerCompanyViewModel
                                  {
                                      QuestionID = ac.QuestionID
                                  }).ToList();


            }

            _questionxLanguage = _questionxLanguage.Where(s => s.LanguageID == 1).ToList();
            CreateNewQuestionViewModel _newQuestion = new CreateNewQuestionViewModel();

            _newQuestion.Languages = _languages;
            _newQuestion.ResponseDataTypeList = new SelectList(_resDataType, "ResponseDataTypeID", "ResponseDataTypeDescription");
            _newQuestion.ProductFamilyList = new SelectList(_productFamily, "ProductFamilyID", "ProductFamilyName");
            _newQuestion.RelatedQuestionList = new SelectList(_questionxLanguage, "QuestionID", "QuestionText");
            _newQuestion.RelatedQuestionResponseList = new SelectList(_resDataType, "ResponseDataTypeID", "ResponseDataTypeDescription");

            return PartialView("_CreateNewQuestionPartial", _newQuestion);
        }

        /// <summary>
        /// Guarda una nueva pregunta en la base de datos
        /// </summary>
        /// <param name="pCreateNewQuestion"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewQuestion(CreateNewQuestionViewModel pCreateNewQuestion)
        {
            int? _reletedQuestionID = 0;
            var _questionID = 0;

            if (pCreateNewQuestion.RelatedQuestionID != 0)
            {
                _reletedQuestionID = pCreateNewQuestion.RelatedQuestionID;
            }

            _questionID = pCreateNewQuestion.CurrentQuestionID;

            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                using (var _transac = db.Database.BeginTransaction())
                {
                    try
                    {

                        string _questionxLanguages = ObjectToXML(pCreateNewQuestion.QuestionsxLanguages);

                        db.NS_spCreateNewQuestion(pCreateNewQuestion.ResponseDataTypeID, (_reletedQuestionID != 0) ? _reletedQuestionID : null,
                            pCreateNewQuestion.RelatedQuestionResponseID.ToString(), _questionxLanguages, (byte)pCreateNewQuestion.ProductFamilyID,
                            pCreateNewQuestion.Order);
                        //db.NS_spQuestion_Insert(pCreateNewQuestion.ResponseDataTypeID,
                        //(_reletedQuestionID != 0) ? _reletedQuestionID : null, pCreateNewQuestion.RelatedQuestionResponseID.ToString());


                        //for (var i = 0; i < pCreateNewQuestion.LanguagesID.Count(); i++)
                        //{
                        //    db.NS_spQuestionByLanguage(_questionID, pCreateNewQuestion.LanguagesID[i],
                        //        pCreateNewQuestion.LanguagesQuestionsText[i]);
                        //}

                        //db.NS_spQuestionxProductFamily_Insert(pCreateNewQuestion.CurrentQuestionID, (byte)pCreateNewQuestion.ProductFamilyID, 
                        //    pCreateNewQuestion.Order);

                        db.SaveChanges();
                        _transac.Commit();
                    }
                    catch (Exception)
                    {
                        _transac.Rollback();
                    }
                }
            }

            return RedirectToAction("ManageQuestion");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pQuestionID"></param>
        /// <returns></returns>
        public ActionResult DeleteQuestion(int pQuestionID, int pProductFamilyID)
        {
            List<AnswerCompanyViewModel> _answerCompany = new List<AnswerCompanyViewModel>();

            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                _answerCompany = (from ac in db.NS_spAnswerCompany_SelectQuestionID()
                                  select new AnswerCompanyViewModel
                                  {
                                      QuestionID = ac.QuestionID
                                  }).ToList();

                List<int> _answerCompanyList = _answerCompany.Select(id => id.QuestionID).ToList();

                if (_answerCompanyList.Contains(pQuestionID))
                {
                    return Json(new { notDelete = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    using (var _transac = db.Database.BeginTransaction())
                    {
                        try
                        {
                            int var = db.NS_spQuestionxProductFamily_Delete(pQuestionID);

                            int otro = db.NS_spQuestionxLanguage_Delete(pQuestionID);

                            int pre = db.NS_spQuestion_Delete(pQuestionID);

                            db.SaveChanges();
                            _transac.Commit();

                            return RedirectToAction("ManageQuestion");
                        }
                        catch (Exception)
                        {
                            _transac.Rollback();
                        }
                    }
                }
            }

            return RedirectToAction("ManageQuestion");
        }

        public string ObjectToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

    }
}