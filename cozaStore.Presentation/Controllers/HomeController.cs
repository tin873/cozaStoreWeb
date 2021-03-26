using cozaStore.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace cozaStore.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductServices _productServices;
        public HomeController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        public async Task<ActionResult> Index()
        {
            var products = await _productServices.GetAsync(filter: b => b.Quantity > 0, orderBy: b => b.OrderBy(x => x.ProductName), page: 1, pageSize: 12);
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}