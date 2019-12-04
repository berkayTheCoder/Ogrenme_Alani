//#define k21
#define k22
#define k23
#define k24
#define k25
#define k26
#define k27

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Entities;
using ShopApp.WebUI.Models;


namespace ShopApp.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductService _productService;
#if k25
        private ICategoryService _categoryService; 
#endif
        public AdminController(
            IProductService productService
#if k25
            ,ICategoryService categoryService
#endif
            )
        {
            _productService = productService;
#if k25
            _categoryService = categoryService;//k25 de eklendi
#endif
        }

        
        

        #region Product Yönetimi

        #region index aksiyonu çalışmaları
#if k21
        public IActionResult Index()
        {
            return View();
        }
#endif
#if k22
        public IActionResult ProductList() 
        {
            return View(new ProductListModel()
            { Products = _productService.GetAll() });
        }
#endif
        #endregion

        #region Create İşlemi
        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();

        }
        [HttpPost]
        public IActionResult CreateProduct(ProductModel model)
        {
            var entity = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                ImageUrl = model.ImageUrl
            };

            _productService.Create(entity);

            return Redirect("index");
        }
        #endregion

#if k23
        #region k23 Editing Product
        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var entity = _productService.GetById((int)id);//k23
            var entity = _productService.GetByIdWithProduct((int)id);//k28
            if (entity == null)
            {
                return NotFound();
            }
            var model = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                SelectedCategory = entity.ProductCategories.Select(i => i.Category).ToList()//k28
            };

            ViewBag.Categories = _categoryService.GetAll();//k28

            return View(model);
        }
        [HttpPost]
        public IActionResult EditProduct(ProductModel model, int[] categoryIds)
        {
            var entity = _productService.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.ImageUrl = model.ImageUrl;
            entity.Price = model.Price;

            _productService.Update(entity, categoryIds);

            return RedirectToAction("ProductList");
        }
        #endregion
#endif

#if k24
        #region k24 Deleting Product
        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productService.GetById(productId);

            if (entity != null)
            {
                _productService.Delete(entity);
            }

            return RedirectToAction("Index");
        }
        #endregion
#endif 
        #endregion


        #region Category Yönetimi
#if k25

        #region Listele-
        public IActionResult CategoryList()
        {
            return View(new CategoryListModel()
            {
                Categories = _categoryService.GetAll()
            });
        }

        #endregion
        #region Oluştur-

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            var entity = new Category()
            {
                Name = model.Name
            };
            _categoryService.Create(entity);

            return RedirectToAction("CategoryList");
        }
        #endregion
        #region Düzenle-

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            //var entity = _categoryService.GetById(id);
            var entity = _categoryService.GetByIdWithProducts(id);//k26
            return View(new CategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,
#if k26
        
                Products = entity.ProductCategories.Select(p=>p.Product).ToList()
#endif
            });
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            var entity = _categoryService.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = model.Name;
            _categoryService.Update(entity);

            return RedirectToAction("CategoryList");
        }
        #endregion
        #region Sil-

        [HttpPost]
        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);

            if (entity != null)
            {
                _categoryService.Delete(entity);
            }

            return RedirectToAction("CategoryList");
        }
        #endregion

#endif
#if k27
        [HttpPost]
        public IActionResult DeleteFromCategory(int categoryId, int productId)
        {
            _categoryService.DeleteFromCategory(categoryId, productId);
            return Redirect("/admin/editcategory/"+categoryId);
        }
#endif
        #endregion
    }
} 
