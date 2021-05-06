using Affidavit.Models.Recommendations;
using DTOs;
using IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Controllers
{
    public class RecommendationController : BaseController
    {
        readonly IVariableService variableService;
        readonly IVariableProductService variableProductService;
        readonly IVariableProductFamilyService variableProductFamilyService;
        readonly IMDSService MDSService;
        readonly IProductFamilyService productFamilyService;
        readonly IProductService productService;
        readonly IProductCompanyService productCompanyService;
        readonly ICompoundVariableService compoundVariableService;

        public RecommendationController(IVariableService pVariableService, IVariableProductService pVariableProductService, IVariableProductFamilyService pVariableProductFamilyService, IMDSService pMDSService, IProductFamilyService pProductFamilyService, IProductService pProductService, IProductCompanyService pProductCompanyService, ICompoundVariableService pCompoundVariableService)
        {
            variableService = pVariableService;
            variableProductService = pVariableProductService;
            variableProductFamilyService = pVariableProductFamilyService;
            MDSService = pMDSService;
            productFamilyService = pProductFamilyService;
            productService = pProductService;
            productCompanyService = pProductCompanyService;
            compoundVariableService = pCompoundVariableService;
        }

        // GET: Recommendation
        public ActionResult Index()
        {
            //EvaluateVariableCompound(variableService.Get(5), 21403, 2251);

            if (Session["userLogin"] == null)
            {
                return RedirectToAction("../Home/LoginAdmin");
            }
            return View();
        }

        // GET: Recommendation/ManageRecommendation
        public ActionResult ManageRecommendation()
        {
            if (Session["userLogin"] == null)
            {
                return RedirectToAction("../Home/LoginAdmin");
            }
            return View("ManageRecommendation");
        }

        // GET: Recommendation/ManageRecommendation
        public ActionResult ManageRecommendationCategory()
        {
            if (Session["userLogin"] == null)
            {
                return RedirectToAction("../Home/LoginAdmin");
            }
            return View("ManageRecommendationCategory");
        }


        //#######################################       PARA VARIABLES            ###############################################

        // GET: Recommendation/ManageRecommendation
        // Este metodo retorna la vista de administración de las variables
        public ActionResult ManageVariables()
        {
            if (Session["userLogin"] == null)
            {
                return RedirectToAction("../Home/LoginAdmin");
            }
            return View("ManageVariables");
        }

        // GET: Recommendation/ManageVariableList
        // Este metodo retorna la lista de TODAS las variables
        public ActionResult ManageVariableList()
        {
            IEnumerable<VariableDTO> _allVariables = variableService.GetAll();
            List<VariableListViewModel> _variableList = new List<VariableListViewModel>();

            foreach (VariableDTO aux in _allVariables)
            {
                string type = "";
                switch (aux.Type)
                {
                    case (int)VariableType.Primary:
                        type = VariableType.Primary.ToString();
                        break;
                    case (int)VariableType.Compound:
                        type = VariableType.Compound.ToString();
                        break;
                    case (int)VariableType.CustomerAttribute:
                        type = VariableType.CustomerAttribute.ToString();
                        break;
                }


                _variableList.Add(
                    new VariableListViewModel
                    {
                        VariableID = aux.VariableID,
                        VariableName = aux.Name,
                        VariableType = type,
                        VariableDescription = aux.Description
                    });
            }
            return PartialView("_ManageVariableListPartial", _variableList);
        }

        // GET: Recommendation/ManageVariable (CREAR)
        // Este metodo retorna la vista de creación de las variables
        public ActionResult CreateVariable()
        {
            CreateUpdateVariableViewModel _newVariable = new CreateUpdateVariableViewModel();

            List<SelectorViewModel> _selectorList = GetOperators();
            List<SelectorViewModel> _familiyList = GetProcessedList(productFamilyService.GetAll(), "Families");
            List<SelectorViewModel> _productList = GetProcessedList(productService.GetAll(), "Products");
            List<SelectorViewModel> _variableList = GetActualVariables(true, null);


            _newVariable.FamiliesList = new SelectList(_familiyList, "SelectorID", "SelectorName");
            _newVariable.ProductList = new SelectList(_productList, "SelectorID", "SelectorName");
            _newVariable.SelectorList = new SelectList(_selectorList, "SelectorID", "SelectorName");
            _newVariable.VariableList = new SelectList(_variableList, "SelectorID", "SelectorName");
            _newVariable.IsEditing = false;
            _newVariable.Selector = "";

            return PartialView("_CreateVariablePartial", _newVariable);
        }

        // POST: Recommendation/ManageVariable (CREAR)
        // Este metodo crea la variable en el sistema
        [HttpPost]
        public ActionResult CreateVariable(CreateUpdateVariableViewModel pCreateUpdateModel)
        {
            VariableDTO _newVariable = new VariableDTO();
            string messageError = "";

            //####################################     PASOS SEGÚN EL TIPO        #####################################################
            if (pCreateUpdateModel.Type.Equals(VariableType.Primary))
            {
                _newVariable.Type = (int)VariableType.Primary;
                _newVariable.Name = pCreateUpdateModel.Name;
                _newVariable.Description = pCreateUpdateModel.Description;
                _newVariable.Field = pCreateUpdateModel.Field.ToString();
                _newVariable.Selector = pCreateUpdateModel.Selector;
                _newVariable.SelectAllFamilies = pCreateUpdateModel.SelectAllFamilies;
                _newVariable.SelectAllProducts = pCreateUpdateModel.SelectAllProducts;
                _newVariable.IsCorporate = SolveBooleanoToBool(pCreateUpdateModel.IsCorporate);
                _newVariable.IsCommercial = SolveBooleanoToBool(pCreateUpdateModel.IsCommercial);
                _newVariable.IsSupported = SolveBooleanoToBool(pCreateUpdateModel.IsSupported);
            }
            else if (pCreateUpdateModel.Type.Equals(VariableType.Compound))
            {
                _newVariable.Type = (int)VariableType.Compound;
                _newVariable.Name = pCreateUpdateModel.Name;
                _newVariable.Description = pCreateUpdateModel.Description;
                _newVariable.IsMathExpression = pCreateUpdateModel.Selector.Equals("MATH EXPRESSION") ? true : false;
                _newVariable.Selector = pCreateUpdateModel.Selector;
                _newVariable.MathExpression = _newVariable.IsMathExpression.Value ? pCreateUpdateModel.MathExpression:null;
            }
            else if (pCreateUpdateModel.Type.Equals(VariableType.CustomerAttribute))
            {
                _newVariable.Type = (int)VariableType.CustomerAttribute;
            }
            //####################################     PASOS SEGÚN EL TIPO        #####################################################


            //####################################     PASOS GENERALES        #####################################################

            int _variableID = variableService.AddAndReturnID(_newVariable);

            //####################################     PASOS GENERALES      #####################################################



            //####################################     PASOS SEGÚN EL TIPO        #####################################################
            if (pCreateUpdateModel.Type.Equals(VariableType.Primary))
            {
                if (!pCreateUpdateModel.SelectAllFamilies)
                {
                    messageError = SaveVariableComplementsForPrimary(pCreateUpdateModel.families, _variableID, false);
                    if (messageError != "")
                    {
                        Response.StatusCode = 403;
                        return RedirectToAction("Index", "Error", new { error = Response.StatusCode, message = messageError });
                    }
                }

                if (!pCreateUpdateModel.SelectAllProducts)
                {
                    messageError = SaveVariableComplementsForPrimary(pCreateUpdateModel.products, _variableID, true);
                    if (messageError != "")
                    {
                        Response.StatusCode = 403;
                        return RedirectToAction("Index", "Error", new { error = Response.StatusCode, message = messageError });
                    }
                }
            }
            else if (pCreateUpdateModel.Type.Equals(VariableType.Compound))
            {
                SaveVariableComplementsForCompound(_newVariable, pCreateUpdateModel, _variableID);
            }

            //####################################     PASOS SEGÚN EL TIPO        #####################################################

            MDSService.CommitChangesAffidavit();

            return RedirectToAction("ManageVariables");
        }

        // GET: Recommendation/ManageVariable (EDITAR)
        // Este metodo retorna la vista de edición de las variables
        public ActionResult EditVariable(int pVariableID)
        {
            CreateUpdateVariableViewModel _newVariable = new CreateUpdateVariableViewModel();
            _newVariable.SelectorList = new SelectList(new List<SelectorViewModel>(), "SelectorID", "SelectorName");
            _newVariable.FamiliesList = new SelectList(new List<SelectorViewModel>(), "SelectorID", "SelectorName");
            _newVariable.ProductList = new SelectList(new List<SelectorViewModel>(), "SelectorID", "SelectorName");
            _newVariable.VariableList = new SelectList(new List<SelectorViewModel>(), "SelectorID", "SelectorName");

            VariableDTO actualVariable = variableService.Get(pVariableID);
            //####################################     PARA EVALUAR        #####################################################
            //EvaluateVariablePrimary(actualVariable, 21403, 2251);
            //####################################     PARA EVALUAR        #####################################################


            if (actualVariable.Type == (int)VariableType.Primary)
            {
                _newVariable.Type = VariableType.Primary;
            }
            else if (actualVariable.Type == (int)VariableType.Compound)
            {
                _newVariable.Type = VariableType.Compound;
            }

            else if (actualVariable.Type == (int)VariableType.CustomerAttribute)
            {
                _newVariable.Type = VariableType.CustomerAttribute;
            }


            //####################################     PASOS SEGÚN EL TIPO        #####################################################
            if (actualVariable.Type == (short)VariableType.Primary)
            {
                List<SelectorViewModel> _selectorList = GetOperators();
                List<SelectorViewModel> _familiyList = GetProcessedList(productFamilyService.GetAll(), "Families");
                List<SelectorViewModel> _productList = GetProcessedList(productService.GetAll(), "Products");


                _newVariable.Name = actualVariable.Name;
                _newVariable.Description = actualVariable.Description;
                _newVariable.SelectAllFamilies = actualVariable.SelectAllFamilies.HasValue ? actualVariable.SelectAllFamilies.Value : false;
                _newVariable.SelectAllProducts = actualVariable.SelectAllProducts.HasValue ? actualVariable.SelectAllProducts.Value : false;
                _newVariable.IsCommercial = SolveBoolToBooleano(actualVariable.IsCommercial);
                _newVariable.IsCorporate = SolveBoolToBooleano(actualVariable.IsCorporate);
                _newVariable.IsSupported = SolveBoolToBooleano(actualVariable.IsSupported);
                _newVariable.FamiliesList = new SelectList(_familiyList, "SelectorID", "SelectorName");
                _newVariable.ProductList = new SelectList(_productList, "SelectorID", "SelectorName");
                _newVariable.SelectorList = new SelectList(_selectorList, "SelectorID", "SelectorName");
                _newVariable.CustomerVariable = actualVariable.CustomerVariable;
                _newVariable.Selector = actualVariable.Selector;


                if (!_newVariable.SelectAllFamilies)
                {
                    var myFamilies = variableProductFamilyService.GetByVariableID((short)pVariableID);

                    if (myFamilies != null && myFamilies.Count() > 0)
                    {
                        _newVariable.families = new List<string>();
                        foreach (VariableProductFamilyDTO aux in myFamilies)
                        {
                            _newVariable.families.Add(aux.ProductFamily.ProductFamilyName + " " + "[" + aux.ProductFamilyID + "]");
                        }
                    }
                }

                if (!_newVariable.SelectAllProducts)
                {
                    var myProducts = variableProductService.GetByVariableID((short)pVariableID);

                    if (myProducts != null && myProducts.Count() > 0)
                    {
                        _newVariable.products = new List<string>();
                        foreach (VariableProductDTO aux in myProducts)
                        {
                            _newVariable.products.Add(aux.Product.ProductFamily.ProductFamilyName + " " + aux.Product.ProductVersionGroup + " - " + aux.Product.ProductNameDisplay + " " + "[" + aux.ProductID + "]");
                        }
                    }
                }


                if (actualVariable.Field == Field.Both.ToString())
                {
                    _newVariable.Field = Field.Both;
                }
                else if (actualVariable.Field == Field.Cal.ToString())
                {
                    _newVariable.Field = Field.Cal;
                }

                else if (actualVariable.Field == Field.Normal.ToString())
                {
                    _newVariable.Field = Field.Normal;
                }
            }
            else if (actualVariable.Type == (short)VariableType.Compound)
            {
                List<SelectorViewModel> _variableList = GetActualVariables(false, pVariableID);
                List<SelectorViewModel> _selectorList = GetOperators();

                _newVariable.Name = actualVariable.Name;
                _newVariable.Description = actualVariable.Description;
                _newVariable.VariableList = new SelectList(_variableList, "SelectorID", "SelectorName");
                _newVariable.SelectorList = new SelectList(_selectorList, "SelectorID", "SelectorName");
                _newVariable.Selector = actualVariable.Selector;

                if(actualVariable.IsMathExpression.Value)
                {
                    _newVariable.MathExpression = actualVariable.MathExpression;
                }
                else
                {
                    var compoundVariableList = compoundVariableService.GetAllByVariableID(actualVariable.VariableID);

                    if(compoundVariableService!=null)
                    {
                        List<string> stringList = new List<string>();

                        foreach(var aux in compoundVariableList)
                        {
                            string compoundText = string.Format("{{{0} [{1}]}}|({2}) {3}", aux.AassociateVariable.Name, aux.AassociateVariable.VariableID, GetVariableType(aux.AassociateVariable.Type), aux.AassociateVariable.Name);
                            stringList.Add(compoundText);
                            //string compound
                            //var currentVariable = compoundVariableService.GetByVariableIDAndAssociatedVariable()
                            //stringList.Add();
                        }
                        _newVariable.VariableFuntionList = stringList.ToArray();
                    }
                    //_newVariable.VariableFuntionList
                    //< option value = "{Primaria 1 (51)}" > (PV)Primaria 1 </ option >

                }

            }
            else if (actualVariable.Type == (short)VariableType.CustomerAttribute)
            {

            }
            //####################################     PASOS SEGÚN EL TIPO        #####################################################


            //####################################       PASOS GENERALES          #####################################################

            _newVariable.VariableID = actualVariable.VariableID;
            _newVariable.IsEditing = true;

            //####################################       PASOS GENERALES          #####################################################


            return PartialView("_EditVariablePartial", _newVariable);
        }

        // POST: Recommendation/ManageVariable (EDITAR)
        // Este método edita la variable en el sistema
        [HttpPost]
        public ActionResult EditVariable(CreateUpdateVariableViewModel pCreateUpdateModel)
        {
            VariableDTO _newVariable = new VariableDTO();
            VariableDTO _pastVariable = variableService.Get(pCreateUpdateModel.VariableID);
            string messageError = "";

            _newVariable.VariableID = pCreateUpdateModel.VariableID;

            //####################################     PASOS SEGÚN EL TIPO        #####################################################
            if (pCreateUpdateModel.Type.Equals(VariableType.Primary))
            {
                _newVariable.Type = (int)VariableType.Primary;
                _newVariable.Name = pCreateUpdateModel.Name;
                _newVariable.Description = pCreateUpdateModel.Description;
                _newVariable.Field = pCreateUpdateModel.Field.ToString();
                _newVariable.Selector = pCreateUpdateModel.Selector;
                _newVariable.SelectAllFamilies = pCreateUpdateModel.SelectAllFamilies;
                _newVariable.SelectAllProducts = pCreateUpdateModel.SelectAllProducts;
                _newVariable.IsCorporate = SolveBooleanoToBool(pCreateUpdateModel.IsCorporate);
                _newVariable.IsCommercial = SolveBooleanoToBool(pCreateUpdateModel.IsCommercial);
                _newVariable.IsSupported = SolveBooleanoToBool(pCreateUpdateModel.IsSupported);

                variableService.UpdateVariable(_newVariable);
            }
            else if (pCreateUpdateModel.Type.Equals(VariableType.Compound))
            {
                _pastVariable.Type = (int)VariableType.Compound;
                _pastVariable.Name = pCreateUpdateModel.Name;
                _pastVariable.Description = pCreateUpdateModel.Description;
                _pastVariable.IsMathExpression = pCreateUpdateModel.Selector.Equals("MATH EXPRESSION") ? true : false;
                _pastVariable.Selector = pCreateUpdateModel.Selector;
                _pastVariable.MathExpression = _pastVariable.IsMathExpression.Value ? pCreateUpdateModel.MathExpression : null;

                variableService.UpdateVariable(_pastVariable);
            }
            else if (pCreateUpdateModel.Type.Equals(VariableType.CustomerAttribute))
            {
                _newVariable.Type = (int)VariableType.CustomerAttribute;
            }
            //####################################     PASOS SEGÚN EL TIPO        #####################################################


            //####################################     PASOS GENERALES        #####################################################

            

            //####################################     PASOS GENERALES        #####################################################



            //####################################     PASOS SEGÚN EL TIPO        #####################################################
            if (pCreateUpdateModel.Type.Equals(VariableType.Primary))
            {
                //Se agregaron nuevas familias
                if (_pastVariable.SelectAllFamilies == true && _newVariable.SelectAllFamilies == false)
                {
                    //SOLO AGREGA
                    messageError = SaveVariableComplementsForPrimary(pCreateUpdateModel.families, _newVariable.VariableID, false);
                    if (messageError != "")
                    {
                        Response.StatusCode = 403;
                        return RedirectToAction("Index", "Error", new { error = Response.StatusCode, message = messageError });
                    }
                }
                //Se pudo haber borrado alguno
                else if (_pastVariable.SelectAllFamilies == false && _newVariable.SelectAllFamilies == false)
                {
                    messageError = EditVariableComplementsForPrimary(pCreateUpdateModel.families, _newVariable.VariableID, false);
                    if (messageError != "")
                    {
                        Response.StatusCode = 403;
                        return RedirectToAction("Index", "Error", new { error = Response.StatusCode, message = messageError });
                    }
                }
                //Se borran todos los que hayan
                else if (_pastVariable.SelectAllFamilies == false && _newVariable.SelectAllFamilies == true)
                {
                    messageError = DeleteVariableComplementsForPrimary(_newVariable.VariableID, false);
                    if (messageError != "")
                    {
                        Response.StatusCode = 403;
                        return RedirectToAction("Index", "Error", new { error = Response.StatusCode, message = messageError });
                    }

                }




                //Se agregaron nuevos productos
                if (_pastVariable.SelectAllProducts == true && _newVariable.SelectAllProducts == false)
                {
                    //SOLO AGREGA
                    messageError = SaveVariableComplementsForPrimary(pCreateUpdateModel.products, _newVariable.VariableID, true);
                    if (messageError != "")
                    {
                        Response.StatusCode = 403;
                        return RedirectToAction("Index", "Error", new { error = Response.StatusCode, message = messageError });
                    }
                }
                //Se pudo haber borrado alguno
                else if (_pastVariable.SelectAllProducts == false && _newVariable.SelectAllProducts == false)
                {
                    messageError = EditVariableComplementsForPrimary(pCreateUpdateModel.products, _newVariable.VariableID, true);
                    if (messageError != "")
                    {
                        Response.StatusCode = 403;
                        return RedirectToAction("Index", "Error", new { error = Response.StatusCode, message = messageError });
                    }
                }
                //Se borran todos los que hayan
                else if (_pastVariable.SelectAllProducts == false && _newVariable.SelectAllProducts == true)
                {
                    messageError = DeleteVariableComplementsForPrimary(_newVariable.VariableID, true);
                    if (messageError != "")
                    {
                        Response.StatusCode = 403;
                        return RedirectToAction("Index", "Error", new { error = Response.StatusCode, message = messageError });
                    }

                }
            }
            else if (pCreateUpdateModel.Type.Equals(VariableType.Compound))
            {
                //Buscar todas las compound variables y eliminarlas de _pastVariable
                DeleteVariableComplementsForCompound(_pastVariable.VariableID);

                //Se guardan las variables otra vez
                SaveVariableComplementsForCompound(_pastVariable, pCreateUpdateModel, _pastVariable.VariableID);


            }
            //####################################     PASOS SEGÚN EL TIPO        #####################################################



            MDSService.CommitChangesAffidavit();

            return RedirectToAction("ManageVariables");
        }

        //#######################################       OTROS MÉTODOS            ###############################################

        //=======================================       PRIMARIAS            //=======================================

        //Este método guarda los productos o familias particulares que se seleccionaron a la hora de crear la variable
        public string SaveVariableComplementsForPrimary(List<string> pComplements, int pVariableID, bool pIsProduct)
        {
            List<string> _idList = new List<string>();
            Char delimiter = '[';
            var _currentVariable = variableService.Get(pVariableID);
            if (pComplements != null && pComplements.Count > 0)
            {
                foreach (string aux in pComplements)
                {
                    var auxList = aux.Split(delimiter);
                    _idList.Add(auxList[auxList.Length - 1].Substring(0, auxList[auxList.Length - 1].Length - 1));
                }

                if (pIsProduct)
                {
                    foreach (string aux in _idList)
                    {
                        VariableProductDTO currentVariableProduct = new VariableProductDTO();
                        ProductDTO currentProduct = productService.Get(Int32.Parse(aux));

                        currentVariableProduct.Product = currentProduct;
                        currentVariableProduct.Variable = _currentVariable;

                        variableProductService.Add(currentVariableProduct);
                    }
                }
                else
                {
                    foreach (string aux in _idList)
                    {
                        VariableProductFamilyDTO currentVariableProductFamily = new VariableProductFamilyDTO();
                        ProductFamilyDTO currentProductFamily = productFamilyService.Get(Int32.Parse(aux));

                        currentVariableProductFamily.ProductFamily = currentProductFamily;
                        currentVariableProductFamily.Variable = _currentVariable;

                        variableProductFamilyService.Add(currentVariableProductFamily);
                    }
                }

            }
            return "";
        }

        //Este método edita los productos o familias particulares que se seleccionaron a la hora de crear la variable
        public string EditVariableComplementsForPrimary(List<string> pComplements, int pVariableID, bool pIsProduct)
        {
            List<string> _idList = new List<string>();
            List<string> _idListNew = new List<string>();
            Char delimiter = '[';
            var _currentVariable = variableService.Get(pVariableID);


            if (pComplements != null && pComplements.Count > 0)
            {
                foreach (string aux in pComplements)
                {
                    var auxList = aux.Split(delimiter);
                    _idList.Add(auxList[auxList.Length - 1].Substring(0, auxList[auxList.Length - 1].Length - 1));
                }

                foreach (string aux in _idList)
                {
                    _idListNew.Add(aux);
                }


                if (pIsProduct)
                {
                    var _variableProductListBD = variableProductService.GetByVariableID((short)pVariableID);
                    var _variableProductList = _variableProductListBD.ToList();

                    //Se identifican los elementos que quedaron igual
                    foreach (string aux in _idList)
                    {
                        int _auxProductID = Int32.Parse(aux);
                        var _currentAuxProduct = _variableProductList.Where(x => x.ProductID == _auxProductID).FirstOrDefault();

                        if (_currentAuxProduct != null)
                        {
                            _variableProductList.Remove(_currentAuxProduct);
                            _idListNew.Remove(aux);
                        }
                    }

                    //Se borran los elementos viejos que ya no estan
                    foreach (var aux in _variableProductList)
                    {
                        variableProductService.Remove(aux);
                    }

                    //Se agregan los nuevos
                    foreach (string aux in _idListNew)
                    {
                        VariableProductDTO currentVariableProduct = new VariableProductDTO();
                        ProductDTO currentProduct = productService.Get(Int32.Parse(aux));

                        currentVariableProduct.Product = currentProduct;
                        currentVariableProduct.Variable = _currentVariable;

                        variableProductService.Add(currentVariableProduct);
                    }

                }
                else
                {
                    var _variableProductFamilyListBD = variableProductFamilyService.GetByVariableID((short)pVariableID);
                    var _variableProductFamilyList = _variableProductFamilyListBD.ToList();


                    //Se identifican los elementos que quedaron igual
                    foreach (string aux in _idList)
                    {
                        int _auxProductFamilyID = Int32.Parse(aux);
                        var _currentAuxProductFamily = _variableProductFamilyList.Where(x => x.ProductFamilyID == _auxProductFamilyID).FirstOrDefault();

                        if (_currentAuxProductFamily != null)
                        {
                            _variableProductFamilyList.Remove(_currentAuxProductFamily);
                            _idListNew.Remove(aux);
                        }
                    }

                    //Se borran los elementos viejos que ya no estan
                    foreach (var aux in _variableProductFamilyList)
                    {
                        variableProductFamilyService.Remove(aux);
                    }

                    //Se agregan los nuevos
                    foreach (string aux in _idListNew)
                    {
                        VariableProductFamilyDTO currentVariableProductFamily = new VariableProductFamilyDTO();
                        ProductFamilyDTO currentProductFamily = productFamilyService.Get(Int32.Parse(aux));

                        currentVariableProductFamily.ProductFamily = currentProductFamily;
                        currentVariableProductFamily.Variable = _currentVariable;

                        variableProductFamilyService.Add(currentVariableProductFamily);
                    }

                }

            }
            else
            {
                if (pIsProduct)
                {
                    var _variableProductListBD = variableProductService.GetByVariableID((short)pVariableID);

                    if (_variableProductListBD == null || _variableProductListBD.Count() > 0)
                    {
                        DeleteVariableComplementsForPrimary(pVariableID, true);
                    }
                }
                else
                {
                    var _variableProductFamilyListBD = variableProductFamilyService.GetByVariableID((short)pVariableID);

                    if (_variableProductFamilyListBD == null || _variableProductFamilyListBD.Count() > 0)
                    {
                        DeleteVariableComplementsForPrimary(pVariableID, false);
                    }
                }
            }

            return "";
        }

        //Este método borra  TODOS los productos o familias para una variable
        public string DeleteVariableComplementsForPrimary(int pVariableID, bool pIsProduct)
        {
            if (pIsProduct)
            {
                var _allMyProducts = variableProductService.GetByVariableID((short)pVariableID);

                if (_allMyProducts != null && _allMyProducts.Count() > 0)
                {
                    foreach (var aux in _allMyProducts)
                    {
                        variableProductService.Remove(aux);
                    }
                }
            }
            else
            {
                var _allMyFamilies = variableProductFamilyService.GetByVariableID((short)pVariableID);

                if (_allMyFamilies != null && _allMyFamilies.Count() > 0)
                {
                    foreach (var aux in _allMyFamilies)
                    {
                        variableProductFamilyService.Remove(aux);
                    }
                }
            }

            return "";
        }

        //=======================================       PRIMARIAS            //=======================================

        //=======================================       COMPUESTAS            //=======================================

        public void SaveVariableComplementsForCompound(VariableDTO _newVariable, CreateUpdateVariableViewModel pCreateUpdateModel, int _variableID)
        {
            if (_newVariable.IsMathExpression.Value)
            {
                //Si es una expresion matematica entonces se recorre el string de la expresión guardandola en registros de base de datos
                //pCreateUpdateModel.MathExpression;

                List<int> signs = new List<int>();

                //Se ubican los signos existentes en la expresion guardando sus posiciones en el vector signs
                if (_newVariable.MathExpression.Length != 0)
                {
                    for (int i = 0; i < _newVariable.MathExpression.Length; i++)
                    {
                        string caracter = _newVariable.MathExpression[i].ToString();

                        if (caracter.Equals("+") ||
                            caracter.Equals("-") ||
                            caracter.Equals("*") ||
                            caracter.Equals("/"))
                        {
                            signs.Add(i);
                        }
                    }
                    signs.Add(_newVariable.MathExpression.Length);
                }


                int begin = 0;
                int final = 0;

                List<string> terms = new List<string>();
                string subString = "";

                //Se ubican los terminos existentes en la expresion guardando sus posiciones en el vector terms
                foreach (int aux in signs)
                {
                    if (aux != 0)
                    {
                        final = aux;
                        subString = _newVariable.MathExpression.Substring(begin, final - begin).Replace("(","").Replace(")", "");
                        terms.Add(subString);

                        begin = final;
                    }
                }

                int j = 0;
                foreach (string term in terms)
                {
                    CompoundVariableDTO aux = new CompoundVariableDTO();

                    //Aqui se asigna la variable a la que pertenece el termino
                    aux.VariableID = (short)_variableID;
                    //Se asigna el orden del termino
                    aux.Order = (short)j;

                    //Si el termino tiene un signo antepuesto
                    if (term[0].ToString().Equals("+") || term[0].ToString().Equals("-") || term[0].ToString().Equals("*") || term[0].ToString().Equals("/"))
                    {
                        //Se guarda el signo porque esta antepuesto
                        aux.MathOperator = term[0].ToString();

                        //Si entra aqui es porque es una variable
                        if (term[1].ToString().Equals("{"))
                        {
                            begin = term.IndexOf("[");
                            final = term.IndexOf("]");
                            //Se extrae el id de la variable del termino
                            string idString = term.Substring(begin + 1, final - begin - 1);
                            int associatedVariableID = Int32.Parse(idString);

                            //Se asigna la variable asociada al termino 
                            aux.AassociateVariableID = (short)associatedVariableID;
                            //Se hace null el valor estatico, porque se trata de un termino con variable 
                            aux.StaticValue = null;
                        }
                        //Si entra aqui es porque es un numero
                        else
                        {
                            //Se hace null la variable asociada, porque se trata de un termino con numero
                            aux.AassociateVariableID = null;
                            //Se extrae el numero del termino
                            aux.StaticValue = Double.Parse(term.Substring(1, term.Length - 1));
                        }

                    }
                    //Si el termino no tiene un signo antepuesto
                    else
                    {
                        //Se hace null el signo porque no esta antepuesto
                        aux.MathOperator = null;
                        //Si entra aqui es porque es una variable
                        if (term[0].ToString().Equals("{"))
                        {
                            begin = term.IndexOf("[");
                            final = term.IndexOf("]");
                            //Se extrae el id de la variable del termino
                            string idString = term.Substring(begin + 1, final - begin - 1);
                            int associatedVariableID = Int32.Parse(idString);

                            //Se asigna la variable asociada al termino 
                            aux.AassociateVariableID = (short)associatedVariableID;
                            //Se hace null el valor estatico, porque se trata de un termino con variable 
                            aux.StaticValue = null;
                        }
                        else
                        {
                            //Se hace null la variable asociada, porque se trata de un termino con numero
                            aux.AassociateVariableID = null;
                            //Se extrae el numero del termino
                            aux.StaticValue = Double.Parse(term);
                        }

                    }
                    compoundVariableService.Add(aux);

                    j++;
                }

            }
            else
            {
                //Si no, entonces se recorre el arreglo con variables que viene para guardarlas 
                foreach (string aux in pCreateUpdateModel.VariableFuntionList)
                {
                    CompoundVariableDTO _newCompound = new CompoundVariableDTO();

                    //Aqui se asigna la variable a la que pertenece la variable del listado
                    _newCompound.VariableID = (short)_variableID;
                    _newCompound.Order = null;
                    _newCompound.StaticValue = null;
                    _newCompound.MathOperator = null;


                    int begin = aux.IndexOf("[");
                    int final = aux.IndexOf("]");
                    //Se extrae el id de la variable del termino
                    string idString = aux.Substring(begin + 1, final - begin - 1);
                    int associatedVariableID = Int32.Parse(idString);
                    //Se asigna la variable asociada (variable del listado)
                    _newCompound.AassociateVariableID = (short)associatedVariableID;

                    compoundVariableService.Add(_newCompound);
                }

            }
        }

        public void DeleteVariableComplementsForCompound(int pVariableID)
        {
            var _allCompoundVariables = compoundVariableService.GetAllByVariableID(pVariableID);

            foreach (var aux in _allCompoundVariables)
            {
                compoundVariableService.Remove(aux);
            }
        }

        //=======================================       COMPUESTAS            //=======================================



        //=======================================       EVALUCACIONES            //=======================================

        public double EvaluateVariablePrimary(VariableDTO pVariable, int pCompanyID, short pCampaignID)
        {
            double resultado = 0;

            string _licenseType = pVariable.Field;
            bool? _licenseTypeBool;

            if (_licenseType.Equals(Field.Normal))
            {
                _licenseTypeBool = false;
            }
            else if (_licenseType.Equals(Field.Cal))
            {
                _licenseTypeBool = true;
            }
            else
            {
                _licenseTypeBool = null;
            }

            bool _allFamilies = pVariable.SelectAllFamilies.HasValue ? pVariable.SelectAllFamilies.Value : false;
            bool _allProducts = pVariable.SelectAllFamilies.HasValue ? pVariable.SelectAllFamilies.Value : false;

            bool? _isCommercial = pVariable.IsCommercial;
            bool? _isCorporate = pVariable.IsCorporate;
            bool? _isSupported = pVariable.IsSupported;


            string operation = pVariable.Selector;

            //Se obtiene todos los productos de una compañia
            var _allProductCompany = productCompanyService.GetByIDAndCampaign(pCompanyID, pCampaignID);

            //Lista para comparar contra los productos de la compañia
            List<ProductDTO> __listProducts = new List<ProductDTO>();


            //Se realizará primero la suma y agregación a la lista de todos los productos que sumen la condición de la variable primaria
            //Para esto se tomara el valor real de _licenseTypeBool si el valor es distinto de null, de lo contrario,
            //se tomara el valor que tenga el producto asignado para que asi sea siempre verdadero y se cumpla la condicion
            //both. Esto mismo se llevara a cabo con Commercial, Corporate y Supported.
            //En un paso posterior se realizara la operación solicitada sobre el resultado de las sumas.

            if (_allProductCompany != null && _allProductCompany.Count() > 0)
            {
                if (_allFamilies && _allProducts)
                {
                    //Toma todos los productos
                    __listProducts = productService.GetAll().ToList();

                    //Para todas las familia OPERA todas las licencias de todos los productos registrados por el cliente
                    //Discriminando por cal,corporate,soporte,comercial
                }
                else if (_allFamilies && !_allProducts)
                {
                    var _listProductsRecommendation = variableProductService.GetByVariableID(pVariable.VariableID);
                    __listProducts = new List<ProductDTO>();

                    //Toma los productos especificos que se agregaron a la variable primaria
                    foreach (var aux in _listProductsRecommendation)
                    {
                        __listProducts.Add(aux.Product);
                    }

                    //OPERA las licencias solo de esos productos especificos
                    //Discriminando por cal,corporate,soporte,comercial
                }
                else if (!_allFamilies && _allProducts)
                {
                    var _listProductFamiliesRecommendation = variableProductFamilyService.GetByVariableID(pVariable.VariableID);
                    __listProducts = new List<ProductDTO>();

                    //Toma los productos de las familias que se agregaron a la variable primaria
                    foreach (var aux in _listProductFamiliesRecommendation)
                    {
                        var _productsXFamily = productService.GetByFamilyID(aux.ProductFamilyID);
                        __listProducts.AddRange(_productsXFamily);
                    }

                    //Por cada familia registrada por el cliente OPERA  todas las licencias de los productos registrados por el cliente
                    //Discriminando por cal,corporate,soporte,comercial
                }
                else if (!_allFamilies && !_allProducts)
                {
                    var _listProductsRecommendation = variableProductService.GetByVariableID(pVariable.VariableID);
                    __listProducts = new List<ProductDTO>();

                    //Toma los productos especificos que se agregaron a la variable primaria
                    foreach (var aux in _listProductsRecommendation)
                    {
                        __listProducts.Add(aux.Product);

                    }

                    //Por cada familia registrada por el cliente OPERA  cada licencias especifica de los productos registrados por el cliente
                    //Discriminando por cal,corporate,soporte,comercial
                }


                List<ProductCompanyDTO> __productCompanyList = new List<ProductCompanyDTO>();

                foreach (var aux in _allProductCompany)
                {
                    var _newCurrentProduct = __listProducts.Where(x => x.ProductID == aux.ProductID).FirstOrDefault();

                    if (_newCurrentProduct != null)
                    {
                        bool _newType = _licenseTypeBool.HasValue ? _licenseTypeBool.Value : _newCurrentProduct.IsCal;
                        bool _newCommercial = _isCommercial.HasValue ? _isCommercial.Value : _newCurrentProduct.IsCommercial;
                        bool _newCorporate = _isCorporate.HasValue ? _isCorporate.Value : _newCurrentProduct.IsCorporate;
                        bool _newSupported = _isSupported.HasValue ? _isSupported.Value : _newCurrentProduct.IsSupported;


                        if (_newCurrentProduct.IsCal == _newType && _newCurrentProduct.IsCommercial == _newCommercial && _newCurrentProduct.IsCorporate == _newCorporate && _newCurrentProduct.IsSupported == _newSupported)
                        {
                            resultado = resultado + aux.InstalledLicenses;
                            __productCompanyList.Add(aux);
                        }
                    }

                }

                //Si no encontro ningun producto con dichas coincidencias
                if (resultado == 0 || __productCompanyList.Count() <= 0)
                {
                    return 0;
                }

                //En un paso posterior se realizara la operación solicitada sobre el resultado de las sumas.
                switch (operation)
                {
                    case "SUM":
                        //Aqui ya se sumaron los resultados
                        return resultado;
                    case "MAXIMUM":
                        return __productCompanyList.Max(x => x.InstalledLicenses);
                    case "MINIMUM":
                        return __productCompanyList.Min(x => x.InstalledLicenses);
                    case "AVERAGE":
                        return __productCompanyList.Average(x => x.InstalledLicenses);
                }

            }
            return resultado;
        }

        public double EvaluateVariableCompound(VariableDTO pVariable, int pCompanyID, short pCampaignID)
        {
            double resultado = 0;

            //Si es una expresion matematica
            if(pVariable.IsMathExpression.Value)
            {
                string _mathExpression = pVariable.MathExpression;

                int inicioCorchetes = -1;
                int finalCorchetes = -1;
                int inicioParentesis = -1;
                int finalParentesis = -1;
                //Se extrae el id de la variable del termino

                inicioCorchetes = _mathExpression.IndexOf("{");
                finalCorchetes = _mathExpression.IndexOf("}");

                while (inicioCorchetes != -1 && finalCorchetes != -1)
                {                    

                    string variableString = _mathExpression.Substring(inicioCorchetes, finalCorchetes - inicioCorchetes +1);

                    inicioParentesis = variableString.IndexOf("[");
                    finalParentesis = variableString.IndexOf("]");

                    string variableIdFromString = variableString.Substring(inicioParentesis + 1, finalParentesis - inicioParentesis - 1);

                    VariableDTO currentVariable = variableService.Get(Int32.Parse(variableIdFromString));

                    double result = 0;

                    if(currentVariable!=null)
                    {
                        //Si es primaria
                        if (currentVariable.Type == 0)
                        {
                            result = EvaluateVariablePrimary(currentVariable, pCompanyID, pCampaignID);
                        }
                        //Si es compuesta 
                        else if (currentVariable.Type == 1)
                        {
                            result = EvaluateVariableCompound(currentVariable, pCompanyID, pCampaignID);
                        }
                    }

                    _mathExpression = _mathExpression.Replace(variableString, result.ToString());

                    inicioCorchetes = _mathExpression.IndexOf("{");
                    finalCorchetes = _mathExpression.IndexOf("}");
                }

                //Se calcula la evaluacion de dicha expresión matematica
                resultado = Convert.ToDouble(new DataTable().Compute(_mathExpression, null));

                return resultado;

            }
            //Si es una funcion (MAX, MIN, AVG)
            else
            {
                var _allFunctionVariables = compoundVariableService.GetAllByVariableID(pVariable.VariableID);
                List<double> _allFunctionVariablesResult = new List<double>();

                foreach (var aux in _allFunctionVariables)
                {
                    //Si es primaria 
                    if(aux.AassociateVariable.Type == 0)
                    {
                        _allFunctionVariablesResult.Add(EvaluateVariablePrimary(aux.AassociateVariable,pCompanyID,pCampaignID));
                    }
                    //Si es compuesta 
                    else if (aux.AassociateVariable.Type == 1)
                    {
                        _allFunctionVariablesResult.Add(EvaluateVariableCompound(aux.AassociateVariable, pCompanyID, pCampaignID));
                    }
                }

                //En este punto ya deberia tener todas las variables resueltas
                switch (pVariable.Selector)
                {
                    case "SUM":
                        //Aqui ya se sumaron los resultados
                        return _allFunctionVariablesResult.Sum();
                    case "MAXIMUM":
                        return _allFunctionVariablesResult.Max();
                    case "MINIMUM":
                        return _allFunctionVariablesResult.Min();
                    case "AVERAGE":
                        return _allFunctionVariablesResult.Average();
                }
            }

            return resultado;
        }

        //=======================================       EVALUCACIONES            //=======================================

        //#######################################       /OTROS MÉTODOS            ###############################################
        //#######################################       /PARA VARIABLES            ###############################################

        //#######################################      MÉTODOS GENERALES            ###############################################
        public List<SelectorViewModel> GetOperators()
        {
            List<SelectorViewModel> _selectorList = new List<SelectorViewModel>();

            _selectorList.Add(
                new SelectorViewModel
                {
                    SelectorID = "MATH EXPRESSION",
                    SelectorName = "MATH EXPRESSION"
                }
            );

            _selectorList.Add(
                new SelectorViewModel
                {
                    SelectorID = "SUM",
                    SelectorName = "SUM"
                }
            );
            _selectorList.Add(
                new SelectorViewModel
                {
                    SelectorID = "MAXIMUM",
                    SelectorName = "MAXIMUM"
                }
            );
            _selectorList.Add(
                new SelectorViewModel
                {
                    SelectorID = "MINIMUM",
                    SelectorName = "MINIMUM"
                }
            );
            _selectorList.Add(
                new SelectorViewModel
                {
                    SelectorID = "AVERAGE",
                    SelectorName = "AVERAGE"
                }
            );

            return _selectorList;
        }

        //obtiene Variables primarias, coompuestas o de usuario existentes
        public List<SelectorViewModel> GetActualVariables(Boolean pIsCreating, int? pVariableId)
        {
            List<SelectorViewModel> _variableList = new List<SelectorViewModel>();

            var _allVariables = variableService.GetAll().OrderBy(x => x.Type);

            if (_allVariables != null && _allVariables.Count() > 0)
            {
                if (pIsCreating)
                {
                    foreach (VariableDTO actualVar in _allVariables)
                    {
                        _variableList.Add(
                            new SelectorViewModel
                            {
                                SelectorID = string.Format("{{{1} [{2}]}}", GetVariableType(actualVar.Type), actualVar.Name, actualVar.VariableID),
                                SelectorName = string.Format("({0}) {1}", GetVariableType(actualVar.Type), actualVar.Name, actualVar.VariableID)
                            }
                        );
                    }
                }
                else
                {
                    var _allVariablesWithoutIncomingVariable = _allVariables.Where(x => x.VariableID != pVariableId);
                    foreach (VariableDTO actualVar in _allVariablesWithoutIncomingVariable)
                    {
                        _variableList.Add(
                            new SelectorViewModel
                            {
                                SelectorID = string.Format("{{{1} [{2}]}}", GetVariableType(actualVar.Type), actualVar.Name, actualVar.VariableID),
                                SelectorName = string.Format("({0}) {1}", GetVariableType(actualVar.Type), actualVar.Name, actualVar.VariableID)
                            }
                        );
                    }                    
                }
            }

            return _variableList;
        }

        public string GetVariableType(short pVarType)
        {
            string _typeLetter;
            switch (pVarType)
            {
                case 0:
                    _typeLetter = "PV";
                    break;
                case 1:
                    _typeLetter = "CV";
                    break;
                case 2:
                    _typeLetter = "UV";
                    break;
                default:
                    _typeLetter = "";
                    break;
            }
            return _typeLetter;

        }

        public List<SelectorViewModel> GetProcessedList(Object pList, string pType)
        {
            List<SelectorViewModel> _list = new List<SelectorViewModel>();
            switch (pType)
            {
                case "Products":
                    foreach (ProductDTO aux in (List<ProductDTO>)pList)
                    {
                        _list.Add(
                            new SelectorViewModel
                            {
                                SelectorID = aux.ProductID.ToString() + "|" + aux.ProductFamilyID,
                                SelectorName = aux.ProductFamily.ProductFamilyName + " " + aux.ProductVersionGroup + " - " + aux.ProductNameDisplay
                            }
                        );
                    }
                    return _list;
                case "Families":
                    foreach (ProductFamilyDTO aux in (List<ProductFamilyDTO>)pList)
                    {
                        _list.Add(
                            new SelectorViewModel
                            {
                                SelectorID = aux.ProductFamilyID.ToString(),
                                SelectorName = aux.ProductFamilyName
                            }
                        );
                    }
                    return _list;
                default:
                    return _list;
            }
        }

        public bool? SolveBooleanoToBool(Booleano pToResolve)
        {

            switch (pToResolve)
            {
                case Booleano.True:
                    return true;
                case Booleano.False:
                    return false;
                default:
                    return null;
            }
        }

        public Booleano SolveBoolToBooleano(bool? pToResolve)
        {
            switch (pToResolve)
            {
                case false:
                    return Booleano.False;
                case true:
                    return Booleano.True;
                default:
                    return Booleano.Both;
            }
        }
        //#######################################      /MÉTODOS GENERALES            ###############################################
    }
}