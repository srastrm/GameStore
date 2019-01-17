using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {

        // visar alla kategorier

        public ActionResult Index()
        {
            var viewModel = new ProductIndexViewModel();
            using (var db = new WebStoreModel())
            {
                viewModel.Categories.AddRange(db.ProductCategories.Select(pc => new ProductIndexViewModel.ProductCategoryListViewModel
                {
                    CategoryId = pc.CategoryId,
                    Name = pc.Name,

                }));
            }

            return View(viewModel);
        }


        // söker i produktnamn och beskrivning

        [HttpGet]
        public ActionResult Search(string SearchProduct, string SearchDescription)
        {
            using (var db = new WebStoreModel())
            {
                var viewModel = new ProductIndexViewModel
                {
                    SearchProduct = SearchProduct,
                    SearchDescription = SearchDescription
                };

                viewModel.Products.AddRange(db.Products.Select(p => new ProductIndexViewModel.ProductListViewModel
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ProductId = p.ProductId,

                }).ToList().Where(x => Matches(x, SearchProduct, SearchDescription)));

                return View("SearchResult", viewModel);
            }
        }

        bool Matches(ProductIndexViewModel.ProductListViewModel product, string SearchProduct, string SearchDescription)
        {
            if (!string.IsNullOrEmpty(SearchProduct))
            {
                SearchProduct = SearchProduct.ToLower();
                if (!product.Name.ToLower().Contains(SearchProduct)) return false;
            }

            if (!string.IsNullOrEmpty(SearchDescription))
            {
                SearchDescription = SearchDescription.ToLower();
                if (!product.Description.ToLower().Contains(SearchDescription)) return false;
            }

            return true;
        }


        // visar sökresultaten

        public ActionResult SearchResult()
        {
            var viewModel = new ProductIndexViewModel();
            using (var db = new WebStoreModel())
            {
                viewModel.Products.AddRange(db.Products.Select(p => new ProductIndexViewModel.ProductListViewModel
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,

                }));
            }

            return View(viewModel);
        }


        // visar alla produkter i vald kategori

        public ActionResult Category (int id, string sort)
        {
            using (var db = new WebStoreModel())
            {
                var category = db.ProductCategories.FirstOrDefault(c => c.CategoryId == id);
                var product = db.Products.FirstOrDefault(p => p.CategoryId == id);
                var viewModel = new ProductCategoryViewModel
                {

                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Products = category.Products.ToList(),
                    CategoryName = category.Name,

                };

                if (sort == "NameAsc")
                    viewModel.Products = viewModel.Products.OrderBy(p => p.Name).ToList();
                else if (sort == "NameDesc")
                    viewModel.Products = viewModel.Products.OrderByDescending(p => p.Name).ToList();

                if (sort == "PriceAsc")
                viewModel.Products = viewModel.Products.OrderBy(p => p.Price).ToList();
                else if (sort == "PriceDesc")
                viewModel.Products = viewModel.Products.OrderByDescending(p => p.Price).ToList();

                viewModel.CurrentSort = sort;

                return View(viewModel);
            }
        }


        // visa detaljer om vald produkt

        public ActionResult View (int id)
        {
            using (var db = new WebStoreModel())
            {
                var product = db.Products.FirstOrDefault(p => p.ProductId == id);
                var viewModel = new ProductViewViewModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                };

                return View(viewModel);
            }
        }
        
        
        // visar alla produkter

        public ActionResult Products(string sort)
        {
            var viewModel = new ProductIndexViewModel();
            using (var db = new WebStoreModel())
            {
                viewModel.Products.AddRange(db.Products.Select(p => new ProductIndexViewModel.ProductListViewModel
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,

                }));
            }

            if (sort == "NameAsc")
                viewModel.Products = viewModel.Products.OrderBy(p => p.Name).ToList();
            else if (sort == "NameDesc")
                viewModel.Products = viewModel.Products.OrderByDescending(p => p.Name).ToList();

            if (sort == "PriceAsc")
                viewModel.Products = viewModel.Products.OrderBy(p => p.Price).ToList();
            else if (sort == "PriceDesc")
                viewModel.Products = viewModel.Products.OrderByDescending(p => p.Price).ToList();

            viewModel.CurrentSort = sort;

            return View(viewModel);
        }


        // skapa en ny produkt

        [HttpGet]
        public ActionResult CreateProduct()
        {
            using (var db = new WebStoreModel())
            {
                var viewModel = new ProductCreateProductViewModel();
                viewModel.Categories = db.ProductCategories.ToList();
                return View(viewModel);
            }
                
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ProductManager")]
        public ActionResult CreateProduct(ProductCreateProductViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new WebStoreModel())
            {
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = model.SelectedCategoryId,
                };

                db.Products.Add(product);
                db.SaveChanges();
            }

            return RedirectToAction("Products");
        }


        // editera vald produkt

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            using (var db = new WebStoreModel())
            {
                var product = db.Products.FirstOrDefault(p => p.ProductId == id);
                
                var viewModel = new ProductEditViewModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ProductId = product.ProductId,
                    Categories = db.ProductCategories.ToList()
            };

                return View(viewModel);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ProductManager")]
        public ActionResult EditProduct(ProductEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new WebStoreModel())
            {
                var product = db.Products.FirstOrDefault(p => p.ProductId == model.ProductId);
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.CategoryId = model.SelectedCategoryId;

                db.SaveChanges();
            }

            return RedirectToAction("Products");
        }


        // radera vald produkt

        [Authorize(Roles = "Admin, ProductManager")]
        public ActionResult DeleteProduct(int id)
        {
            using (var db = new WebStoreModel())
            {
                var product = db.Products.FirstOrDefault(p => p.ProductId == id);
                db.Products.RemoveRange(new[] { product });
                db.SaveChanges();

                return RedirectToAction("Products");
            }
        }


        // skapa en ny kategori

        [HttpGet]
        public ActionResult CreateCategory()
        {
            using (var db = new WebStoreModel())
            {
                var viewModel = new CreateCategoryViewModel();
                return View(viewModel);
            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin, ProductManager")]
        public ActionResult CreateCategory(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new WebStoreModel())
            {
                var category = new ProductCategory
                {
                    Name = model.Name
                };

                db.ProductCategories.Add(category);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // ändra namn på vald kategori

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            using (var db = new WebStoreModel())
            {
                var category = db.ProductCategories.FirstOrDefault(c => c.CategoryId == id);
                var model = new EditCategoryViewModel
                {
                    Name = category.Name,
                    CategoryId = category.CategoryId

                };
                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditCategory(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new WebStoreModel())
            {
                var category = db.ProductCategories.FirstOrDefault(c => c.CategoryId == model.CategoryId);
                category.Name = model.Name;
                category.CategoryId = model.CategoryId;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // ta bort vald kategori

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCategory(int id)
        {
            using (var db = new WebStoreModel())
            {
                var category = db.ProductCategories.FirstOrDefault(c => c.CategoryId == id);
                db.ProductCategories.RemoveRange(new[] { category });
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

    }
}