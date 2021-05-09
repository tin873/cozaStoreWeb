using cozaStoreWeb.Models;
using System.Linq;
using System.Web.Mvc;

namespace cozaStoreWeb.Areas.Admin.Controllers
{
    public class ContactsController : Controller
    {
        private cozaStoreDB db = new cozaStoreDB();

        // GET: Admin/Contacts
        public ActionResult Index()
        {
            if (Session["IDAdmin"] != null)
            {
                return View(db.Contacts.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
        } 
    }
}
