using cozaStoreWeb.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace cozaStoreWeb.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private cozaStoreDB db = new cozaStoreDB();

        // GET: Admin/Orders
        public ActionResult Index()
        {
            if (Session["IDAdmin"] != null)
            {
                var orders = db.Orders.Include(o => o.User);
                return View(orders.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
        }   
        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", order.UserID);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,OrderDate,UserID,Address,Phone,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FullName", order.UserID);
            return View(order);
        }

       
    }
}
