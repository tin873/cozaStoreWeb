using cozaStoreWeb.Models;
using System.Web.Mvc;

namespace cozaStoreWeb.Areas.Admin.Controllers
{
    public class AdminIDController : Controller
    {
        // GET: Admin/AdminID
        // GET: Admin/AdminID
        cozaStoreDB db = new cozaStoreDB();
        // GET: Admin/Home

        public ActionResult Indexs()
        {
            if (Session["IDAdmin"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home", new { area = ""});
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home", new { area = "" });
        }
    }
}