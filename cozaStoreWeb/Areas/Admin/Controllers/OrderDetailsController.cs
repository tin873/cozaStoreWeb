using cozaStoreWeb.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace cozaStoreWeb.Areas.Admin.Controllers
{
    public class OrderDetailsController : Controller
    {
        private cozaStoreDB db = new cozaStoreDB();

        // GET: Admin/OrderDetails
        public ActionResult Index(int id)
        {
            var orderDetails = db.OrderDetails.Where( h => h.OrderID == id).Include(o => o.Order).Include(o => o.Product);
            return View(orderDetails.ToList());
        }
    }
}
