using cozaStore.BusinessLogicLayer;
using System.Linq;
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
        public ActionResult Index()
        {
            var products = _productServices.GetTop(orderBy: x => x.OrderBy(p => p.Quantity));
            return View(products);
        }

        public PartialViewResult _TopProductNew()
        {
            var products = _productServices.GetTop(orderBy: x => x.OrderByDescending(p => p.Quantity));
            return PartialView(products);
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