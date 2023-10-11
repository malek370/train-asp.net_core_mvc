using BOL.Models;
using BOL.ModelViews;
using DAL.Repositories.Irepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.BOL.Models;
//using System.Web.Mvc;
namespace WebApplication2.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        protected readonly IProdRepository _products;
        protected readonly ICatRepository _categories;
        protected readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProdRepository products , ICatRepository categories,IWebHostEnvironment webHostEnvironment)
        {
            _products = products;
            _categories = categories;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var prods = _products.GetAll();
			ViewBag.cats = _categories.GetItems();
			return View(prods);
        }
        public IActionResult Upsert(int? id)
        {
            ViewBag.cats = _categories.GetItems();

			if (id == null)
            {
                return View();
            }
            Product product = _products.Get((int)id);
            if (product == null) { return NotFound(); }
            return View(product);
			
        }

        [HttpPost]
        public IActionResult Upsert(Product product,IFormFile? file)
        {
			var cats = _categories.GetItems();

			if (!cats.Keys.ToList().Contains(product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "give an existing category");
            }
            if (ModelState.IsValid)
            {
                if (file != null)
                {
					var wwwroot = _webHostEnvironment.WebRootPath;
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filepath=Path.Combine(wwwroot,@"img\prod");
                    //delete if exist to be done
                    if (!string.IsNullOrEmpty(product.imgURL))
                    {
                        var oldImgUrl = Path.Combine(wwwroot, product.imgURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImgUrl)) { System.IO.File.Delete(oldImgUrl); } 
                    }
                    using(var fileStream = new FileStream(Path.Combine(filepath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    product.imgURL = Path.Combine(@"\img\prod", filename);

				}
                if (product.Id == 0) { _products.Add(product); }
                else { _products.Update(product); }
                
                TempData["success"] = $"Product {product.Name} created or updated";
                return RedirectToAction("Index");
            }
            ViewBag.cats = cats; 
			return View();
        }

        public ActionResult Delete(int id) 
        {
            var p = _products.Get(id);
			if ( p== null) { return NotFound(); }
            try
            {
				_products.Remove(id);
				_products.Save();
				TempData["success"] = $"Product {p.Name} deleted";
				return RedirectToAction("Index");
			}
            catch (Exception ex) {
				TempData["fail"] = ex.Message;
				return RedirectToAction("Index");
			}
        }

		

	}
}