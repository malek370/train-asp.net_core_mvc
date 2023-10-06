using BOL.Models;
using DAL.Repositories.Irepositories;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.BOL.Models;

namespace WebApplication2.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        protected readonly IProdRepository _products;
        protected readonly ICatRepository _categories;
        public ProductController(IProdRepository products , ICatRepository categories)
        {
            _products = products;
            _categories = categories;
        }

        public IActionResult Index()
        {
            var prods = _products.GetAll();
            var cc = _categories.GetItems();
			ViewBag.cats = cc;
			return View(prods);
        }
        public IActionResult Creat()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Creat(Product p)
        {
            if (p == null) { return NotFound(); }
            if (ModelState.IsValid)
            {
                _products.Add(p);
                _products.Save();
                TempData["success"] = $"Product {p.Name} created";
                return RedirectToAction("Index");
            }
                return View();
        }

        public ActionResult Delete(int id) 
        {
            if (id == null) { return NotFound(); }
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

		public IActionResult Edit(int id)
		{
			if (id == null) { return NotFound(); }
			var p = _products.Get(id);
			if (p == null) { return NotFound(); }
			return View(p);
		}
		[HttpPost]
		public IActionResult Edit(Product p)
		{
			if (p == null) { return NotFound(); }
			if (ModelState.IsValid)
			{
				_products.Update(p);
				_products.Save();
				TempData["success"] = $"Product {p.Name} Updated";
				return RedirectToAction("Index");
			}
			return View();
		}

	}
}