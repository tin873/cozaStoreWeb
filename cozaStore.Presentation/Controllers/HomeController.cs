using cozaStore.BusinessLogicLayer;
using System.Linq;
using System.Web.Mvc;

namespace cozaStore.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly ICategoryServices _category;
        public HomeController(IProductServices productServices, ICategoryServices category)
        {
            _productServices = productServices;
            _category = category;
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

        public PartialViewResult _Footer()
        {
            var categories = _category.GetAll();
            return PartialView(categories);
        }
    }
}