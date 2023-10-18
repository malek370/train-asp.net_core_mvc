using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using WebApplication2.DAL.data;
using WebApplication2.BOL.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Utilities;
using DAL.Repositories.Irepositories;
using Microsoft.AspNetCore.Authorization;
using Utilities;

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Admin)]
    public class CategoryController : Controller
    {
        private readonly ICatRepository _category;
        public CategoryController(ICatRepository catrepo)
        {
            _category = catrepo;
        }

        public IActionResult Index()
        {
            var cats = _category.GetAll();
            return View(cats);
        }

        public IActionResult Creat()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Creat(Category c)
        {
            c.abb = c.abb.ToUpper();
            c.Id = c.abb.toId();
            if (_category.Get(c.Id) != null) { ModelState.AddModelError("abb", "already exist"); }
            if (ModelState.IsValid)
            {
                _category.Add(c);
                _category.Save();
                TempData["success"] = $"category {c.abb} has been created";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int id)
        {
            Category? category = _category.Get(id);
            if (category == null) { return NotFound(); }
            return View(category);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(Category c)
        {


            try
            {

                _category.Update(c);
                _category.Save();
                TempData["success"] = $"category {c.abb} has been changed";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["fail"] = ex.Message;
                return RedirectToAction("Index");
            }

        }
        public async Task<IActionResult> Delete(int id)
        {
            Category? category = _category.Get(id);
            if (category == null) { return NotFound(); }
            try
            {

                _category.Remove(category.Id);
                _category.Save();
                TempData["success"] = $"category {category.abb} has been deleted";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["fail"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
