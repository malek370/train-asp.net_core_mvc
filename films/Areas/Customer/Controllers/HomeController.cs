using BOL.Models;
using DAL.Repositories.Irepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Utilities;
using WebApplication2.BOL.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebApplication2.Areas.Customer.Controllers
{
    [Area("Customer")]
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProdRepository _prodRepository;
        private readonly IComRepo _comRepo;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(ILogger<HomeController> logger, IProdRepository prodRepository, IComRepo comRepo ,UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _prodRepository = prodRepository;
            _comRepo = comRepo;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var prods=_prodRepository.GetAll().ToList();
            return View(prods);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Details(int id)
        {
            var prod=_prodRepository.Get(id);
            if(prod==null) { return NotFound(); }
            else
            {
                ViewBag.prod = prod;
                return View();
            }
        }
        [HttpPost]
		public  IActionResult Details(Commande c)
		{
			string? userId = _userManager.GetUserId(User);
			if (userId==null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
				
				c.IdUser = userId;
				_comRepo.addCom(c);
                TempData["success"] = "product added";
                
            }
            else { TempData["fail"] = "error happened"; }
            return RedirectToAction("Index");
		}

        
		public IActionResult Panier()
		{
            string? userId = _userManager.GetUserId(User);
            if(userId==null) { return NotFound(); }
			var coms = _comRepo.getCom(userId);
            
            List<Product> products = new List<Product>();
            foreach (var c in coms) {
                products.Add(_prodRepository.Get(c.IdProduct));
            }
            
			return View(products);
		}
	}
}