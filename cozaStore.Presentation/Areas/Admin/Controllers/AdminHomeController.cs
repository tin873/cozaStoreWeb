using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cozaStore.Presentation.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            if (Session["fullNameAdmin"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home", new { Area = "" });
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home", new { area = "" });
        }
    }
}