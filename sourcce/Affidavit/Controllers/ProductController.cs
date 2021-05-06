using Affidavit.Models.AdminMDS;
using Affidavit.Models.Product;
using DTOs;
using Entities.DbAffidavit;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Affidavit.Controllers
{
    #if !DEBUG
    [RequireHttps] //apply to all actions in controller
    #endif
    public class ProductController : BaseController
    {        
        private IProductService productService;
        private IProductFamilyService productFamilyService;

        public ProductController(IProductService pProductyService, IProductFamilyService pProductFamilyService)
        {
            productService = pProductyService;
            productFamilyService = pProductFamilyService;
        }

        /// <summary>
        /// Muestra la vista para en maestro de productos
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageProduct()
        {
            if (Session["userLogin"] == null)
            {
                return RedirectToAction("../Home/LoginAdmin");
            }

            var _pageNumber = 1;
            var _pageSize = 10;

            ManageProductViewModel _model = new ManageProductViewModel();
            List<ProductListViewModel> _productList = new List<ProductListViewModel>();

            _productList = DBProductList();

            var _products = _productList.Skip((_pageNumber - 1) * _pageSize).Take(_pageSize).ToList();
            _model.ProductList = _products;

            //_model.ProductList = _productList;
            _model.pageSettings = new Helpers.NSPageSettings();
            _model.pageSettings.dataItems = _productList.Count();
            _model.pageSettings.page = _pageNumber;
            _model.pageSettings.size = _pageSize;
            _model.filters = new ProductFiltersViewModel();
            _model.filters.ProductFamily = "";
            _model.filters.ProductName = "";
            _model.filters.ProductVersion = "";
            _model.filters.ProductVersionGroup = "";

            return View("ManageProduct", _model);
        }

        /// <summary>
        /// hace llamado a store procedure para obtener la lista de productos
        /// </summary>
        /// <returns></returns>
        public List<ProductListViewModel> DBProductList()
        {
            List<ProductListViewModel> _productList = new List<ProductListViewModel>();

            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                _productList = (from p in db.NS_spProduct_Select()
                                select new ProductListViewModel
                                {
                                    ProductID = p.ProductID,
                                    ProductName = p.ProductName,
                                    ProductVersion = p.ProductVersion,
                                    DisplayOrder = p.DisplayOrder,
                                    FamilyName = p.ProductFamilyName,
                                    IsActive = p.IsActive,
                                    IsCal = p.IsCal,
                                    IsCommercial = p.IsCommercial,
                                    IsCorporate = p.IsCorporate,
                                    IsSupported = p.IsSupported,
                                    NameDisplay = p.ProductNameDisplay,
                                    OEMFlag = p.OEMFlag,
                                    OrderVersion = p.OrderVersion,
                                    ProductFamilyID = p.ProductFamilyID,
                                    ProductVersionGroup = p.ProductVersionGroup

                                }).ToList();
            }

            return _productList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pModel"></param>
        /// <returns></returns>
        public ActionResult ManageProductList(ManageProductViewModel pModel)
        {
            return PartialView("_ManageProductListPartial", pModel);
        }

        /// <summary>
        /// Carga formularion para crear un nuevo producto
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateNewProduct()
        {
            List<ProductFamilyViewModel> _productFamily = new List<ProductFamilyViewModel>();

            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                _productFamily = (from pf in db.NS_spProductFamily_SelectAll()
                                  select new ProductFamilyViewModel
                                  {
                                      ProductFamilyID = pf.ProductFamilyID,
                                      ProductFamilyName = pf.ProductFamilyName
                                  }).ToList();
            }

            CreateUpdateProductViewModel _newProduct = new CreateUpdateProductViewModel();

            _newProduct.ProductFamilyList = new SelectList(_productFamily, "ProductFamilyID", "ProductFamilyName");

            return PartialView("_CreateNewProductPartial", _newProduct);
        }

        /// <summary>
        /// guarda un nuevo producto.
        /// </summary>
        /// <param name="pCreateUpdateModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewProduct(CreateUpdateProductViewModel pCreateUpdateModel)
        {
            var OEMFlag = (pCreateUpdateModel.OEMFlag == IsOEM.False) ? false : true;
            var IsActive = (pCreateUpdateModel.IsActive == IsOEM.False) ? false : true;
            var IsCal = (pCreateUpdateModel.IsCal == IsOEM.False) ? false : true;
            var IsCommercial = (pCreateUpdateModel.IsCommercial == IsOEM.False) ? false : true;
            var IsCorporate = (pCreateUpdateModel.IsCorporate == IsOEM.False) ? false : true;
            var IsSupported = (pCreateUpdateModel.IsSupported == IsOEM.False) ? false : true;
            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                db.NS_spProduct_Create(pCreateUpdateModel.ProductName, pCreateUpdateModel.ProductVersion, (byte)pCreateUpdateModel.ProductFamilyID,
                    pCreateUpdateModel.ProductVersionGroup, OEMFlag, pCreateUpdateModel.OrderVersion, IsActive, pCreateUpdateModel.NameDisplay,
                    IsCal, IsCommercial,IsCorporate,IsSupported, pCreateUpdateModel.DisplayOrder
                    );
            }

            return RedirectToAction("ManageProduct");
        }

        /// <summary>
        /// Carga formulario para editar un producto 
        /// </summary>
        /// <param name="pProductID">Id del producto a editar</param>
        /// <returns></returns>
        public ActionResult EditProduct(short pProductID)
        {
            List<ProductFamilyViewModel> _productFamily = new List<ProductFamilyViewModel>();

            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                _productFamily = (from pf in db.NS_spProductFamily_SelectAll()
                                  select new ProductFamilyViewModel
                                  {
                                      ProductFamilyID = pf.ProductFamilyID,
                                      ProductFamilyName = pf.ProductFamilyName
                                  }).ToList();
            }

            //ProductDTO _productDTO = productService.Get(pProductID);
            ProductViewModel _productDTO = new ProductViewModel();
            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                _productDTO = (from p in db.NS_spProduct_SelectID(pProductID)
                               select new ProductViewModel
                               {
                                   ProductID = p.ProductID,
                                   ProductName = p.ProductName,
                                   DisplayOrder = p.DisplayOrder,
                                   IsActive = p.IsActive,
                                   IsCal = p.IsCal,
                                   IsCommercial= p.IsCommercial,
                                   IsCorporate = p.IsCorporate,
                                   IsSupported = p.IsSupported,
                                   OEMFlag = p.OEMFlag,
                                   OrderVersion = p.OrderVersion,
                                   ProductFamilyID = p.ProductFamilyID,
                                   ProductNameDisplay = p.ProductNameDisplay,
                                   ProductVersion = p.ProductVersion,
                                   ProductVersionGroup = p.ProductVersionGroup
                               }).FirstOrDefault();
            }

            CreateUpdateProductViewModel _newProduct = new CreateUpdateProductViewModel();


            _newProduct.ProductFamilyList = new SelectList(_productFamily, "ProductFamilyID", "ProductFamilyName", _productDTO.ProductFamilyID);
            _newProduct.ProductFamilyID = _productDTO.ProductFamilyID;
            _newProduct.ProductID = _productDTO.ProductID;
            _newProduct.ProductName = _productDTO.ProductName;
            _newProduct.ProductVersion = _productDTO.ProductVersion;
            _newProduct.ProductVersionGroup = _productDTO.ProductVersionGroup;
            _newProduct.OrderVersion = _productDTO.OrderVersion;
            _newProduct.NameDisplay = _productDTO.ProductNameDisplay;
            //_newProduct.IsCal = _productDTO.IsCal;
            _newProduct.OrderVersion = _productDTO.OrderVersion;
            _newProduct.DisplayOrder = _productDTO.DisplayOrder;

            switch (_productDTO.IsCal)
            {
                case false:
                    _newProduct.IsCal = IsOEM.False;
                    break;
                case (true):
                    _newProduct.IsCal = IsOEM.True;
                    break;
            }
            switch (_productDTO.IsCommercial)
            {
                case false:
                    _newProduct.IsCommercial = IsOEM.False;
                    break;
                case (true):
                    _newProduct.IsCommercial = IsOEM.True;
                    break;
            }
            switch (_productDTO.IsCorporate)
            {
                case false:
                    _newProduct.IsCorporate = IsOEM.False;
                    break;
                case (true):
                    _newProduct.IsCorporate = IsOEM.True;
                    break;
            }
            switch (_productDTO.IsSupported)
            {
                case false:
                    _newProduct.IsSupported = IsOEM.False;
                    break;
                case (true):
                    _newProduct.IsSupported = IsOEM.True;
                    break;
            }

            switch (_productDTO.OEMFlag)
            {
                case false:
                    _newProduct.OEMFlag = IsOEM.False;
                    break;
                case (true):
                    _newProduct.OEMFlag = IsOEM.True;
                    break;
            }

            switch (_productDTO.IsActive)
            {
                case false:
                    _newProduct.IsActive = IsOEM.False;
                    break;
                case (true):
                    _newProduct.IsActive = IsOEM.True;
                    break;
            }

            return PartialView("_EditProductPartial", _newProduct);
        }

        /// <summary>
        /// actualiza un producto
        /// </summary>
        /// <param name="pUpdateModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateProduct(CreateUpdateProductViewModel pUpdateModel)
        {
            var OEMFlag = (pUpdateModel.OEMFlag == IsOEM.False) ? false : true;
            var IsActive = (pUpdateModel.IsActive == IsOEM.False) ? false : true;
            var IsCal = (pUpdateModel.IsCal == IsOEM.False) ? false : true;
            var IsCommercial = (pUpdateModel.IsCommercial == IsOEM.False) ? false : true;
            var IsCorporate = (pUpdateModel.IsCorporate == IsOEM.False) ? false : true;
            var IsSupported = (pUpdateModel.IsSupported == IsOEM.False) ? false : true;

            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                db.NS_spProduct_Update(pUpdateModel.ProductID, pUpdateModel.ProductName, pUpdateModel.ProductVersion, (byte)pUpdateModel.ProductFamilyID,
                    pUpdateModel.ProductVersionGroup, OEMFlag, pUpdateModel.OrderVersion, IsActive, pUpdateModel.NameDisplay,
                    IsCal,IsCommercial,IsCorporate,IsSupported, pUpdateModel.DisplayOrder);
            }

            return RedirectToAction("ManageProduct");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pFilter"></param>
        /// <returns></returns>
        public PartialViewResult CallBackFilter(int? pPage, int? pPageSize, ProductFiltersViewModel pFilter)
        {
            var _pageNumber = pPage ?? 1;
            var _pageSize = pPageSize ?? 2;

            ManageProductViewModel _model = new ManageProductViewModel();

            var _productList = DBProductList();

            if (!string.IsNullOrEmpty(pFilter.ProductName))
            {
                _productList = _productList.Where(p => p.ProductName.ToUpper().Contains(pFilter.ProductName.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(pFilter.ProductVersion))
            {
                _productList = _productList.Where(p => p.ProductVersion.ToUpper().Contains(pFilter.ProductVersion.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(pFilter.ProductFamily))
            {
                List<short> _idList = new List<short>();
                using (AffidavitModelsConnection db = new AffidavitModelsConnection())
                {
                    _idList = (from pf in db.NS_spProductFamily_Select(pFilter.ProductFamily)
                               select new ProductFamilyViewModel
                               {
                                   ProductFamilyID = pf.ProductFamilyID
                               }).Select(n => n.ProductFamilyID).ToList();
                }

                _productList = _productList.Where(p => _idList.Contains(p.ProductFamilyID)).ToList();

            }

            if (!string.IsNullOrEmpty(pFilter.ProductVersionGroup))
            {
                _productList = _productList.Where(p => p.ProductVersionGroup.ToUpper().Contains(pFilter.ProductVersionGroup.ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(pFilter.Status))
            {
                var _status = (pFilter.Status == "Active") ? true : false;
                _productList = _productList.Where(p => p.IsActive.Equals(_status)).ToList();
            }

            var _products = _productList.Skip((_pageNumber - 1) * _pageSize).Take(_pageSize).ToList();
            _model.ProductList = _products;
            _model.pageSettings = new Helpers.NSPageSettings();
            _model.pageSettings.dataItems = _productList.Count();
            _model.pageSettings.page = _pageNumber;
            _model.pageSettings.size = _pageSize;
            _model.filters = pFilter;

            return PartialView("_ManageProductListPartial", _model);
        }


        /// <summary>
        /// elimina un producto de la base de datos.
        /// </summary>
        /// <param name="pProductID">id del producto a eliminar</param>
        /// <returns></returns>
        public ActionResult DeleteProduct(short pProductID)
        {
            List<ProductCompanyViewModel> _productCompanyList = new List<ProductCompanyViewModel>();
            ProductCompanyViewModel _productCompany = new ProductCompanyViewModel();
            List<short> _productCompanyIDList = new List<short>();

            using (AffidavitModelsConnection db = new AffidavitModelsConnection())
            {
                _productCompanyList = (from pc in db.NS_spProductCompany_Select()
                                       select new ProductCompanyViewModel
                                       {
                                           CampaignID = pc.CampaignID,
                                           CompanyID = pc.CompanyID,
                                           ProductID = pc.ProductID
                                       }).ToList();
            }

            _productCompanyIDList = _productCompanyList.Select(s => s.ProductID).ToList();

            if (_productCompanyIDList.Contains(pProductID))
            {
                return Json(new { notDelete = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                using (AffidavitModelsConnection db = new AffidavitModelsConnection())
                {
                    db.NS_spProduct_Delete(pProductID);
                }
            }
            
            return RedirectToAction("ManageProduct");
        }
    }
}