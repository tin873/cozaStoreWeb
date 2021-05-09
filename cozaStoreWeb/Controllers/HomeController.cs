using cozaStoreWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace cozaStoreWeb.Controllers
{
    public class HomeController : Controller
    {
        cozaStoreDB db = new cozaStoreDB();
        public ActionResult Index(string id, FormCollection data)
        {
            List<Product> products = new List<Product>();
            string name = data["search"];
            string nameHeader = data["searchHeader"];
            if (id == null)
            {
                products = db.Products.Select(h => h).ToList();
            }
            else
            {
                products = db.Products.Where(h => h.CategoryID.ToString().Equals(id.ToString())).Select(p => p).ToList();
            }
            if (!String.IsNullOrEmpty(name))
            {
                products = db.Products.Where(h => h.ProductName.ToLower().Contains(name.ToLower())).Select(p => p).ToList();
            }
            if (!String.IsNullOrEmpty(nameHeader))
            {
                products = db.Products.Where(h => h.ProductName.ToLower().Contains(nameHeader.ToLower())).Select(p => p).ToList();
            }
            return View(products.ToList());
        }
        public ActionResult Products(string id, FormCollection data)
        {
            List<Product> products = new List<Product>();
            string name = data["search"];
            string nameHeader = data["searchHeader"];
            if (id == null)
            {
                products = db.Products.Select(h => h).ToList();
            }
            else
            {
                products = db.Products.Where(h => h.CategoryID.ToString().Equals(id.ToString())).Select(p => p).ToList();
            }
            if (!String.IsNullOrEmpty(name))
            {
                products = db.Products.Where(h => h.ProductName.ToLower().Contains(name.ToLower())).Select(p => p).ToList();
            }
            if (!String.IsNullOrEmpty(nameHeader))
            {
                products = db.Products.Where(h => h.ProductName.ToLower().Contains(nameHeader.ToLower())).Select(p => p).ToList();
            }
            return View(products.ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection dt)
        {
            string email = dt["emaildn"];
            string pass = dt["passdn"];
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(u => u.Email.Equals(email) && u.PassWord.Equals(pass)).ToList();
                if (user.Count() > 0)
                {
                    if (user.FirstOrDefault().RoleID == 2)
                    {
                        Session["fullName"] = user.FirstOrDefault().FullName;
                        Session["address"] = user.FirstOrDefault().Address;
                        Session["phone"] = user.FirstOrDefault().Phone;
                        Session["Email"] = user.FirstOrDefault().Email;
                        Session["UserID"] = user.FirstOrDefault().UserID;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Session["fullNameAdmin"] = user.FirstOrDefault().FullName;
                        Session["EmailAdmin"] = user.FirstOrDefault().Email;
                        Session["IDAdmin"] = user.FirstOrDefault().UserID;
                        return RedirectToAction("Indexs", "AdminID", new { area = "Admin" });
                    }
                }
                else
                {
                    ViewBag.error = "Sai tên đăng nhập hoặc mật khẩu!";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection data)
        {
            string fullName = data["fullNameRgt"];
            string email = data["emailRgt"];
            string pass = data["PassRgt"];
            string adress = data["addressRgt"];
            string phone = data["phoneRgt"];
            var emailcheck = db.Users.Where(u => u.Email.Equals(email));
            if (emailcheck != null)
            {
                ViewBag.error = "Email này đã tồn tại!";
            }
            else
            {
                User user = new User()
                {
                    FullName = fullName,
                    Email = email,
                    PassWord = pass,
                    Address = adress,
                    Phone = phone,
                    RoleID = 2,
                };
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }

            }
            return View();
        }
        public ActionResult ProductDetails(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hang = db.Products.Find(id);
            if (hang == null)
            {
                return HttpNotFound();
            }
            return View(hang);
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(FormCollection data)
        {
            string email = data["email"];
            string msg = data["msg"];
            Contact contact = new Contact()
            {
                Email = email,
                Description = msg,
            };

            db.Contacts.Add(contact);
            db.SaveChanges();
            return RedirectToAction("Contact");
        }
        public PartialViewResult _Footer()
        {
            var danhMuc = db.Categories.Select(h => h);

            return PartialView(danhMuc);
        }
        public PartialViewResult _Menu()
        {
            var danhMuc = db.Categories.Select(h => h);

            return PartialView(danhMuc);
        }

        public PartialViewResult _Search()
        {
            return PartialView();
        }

        public PartialViewResult _RelatedProducts(int id)
        {
            var products = db.Products.Where(p => p.CategoryID == id).Select(h => h);
            return PartialView(products);
        }
        public PartialViewResult _LoadMore()
        {
            return PartialView();
        }
        public PartialViewResult _Size()
        {
            //var size = db.ColorsSizes.Where(h => h.ProductID == idproduct).ToList();
            var sizes = db.Sizes.ToList();
            return PartialView(sizes);
        }
        public PartialViewResult _Color()
        {
            var color = db.Colors.ToList();
            return PartialView(color);
        }

        [HttpPost]
        public ActionResult _Comment(FormCollection data)
        {
            var review = data["review"];
            var name = data["name"];
            var email = data["email"];
            var productID = data["productID"];
            char[] x = productID.ToCharArray();
            string a = "";
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] >= '0' && x[i] <= '9')
                {
                    a += x[i];
                }
            }
            int id = Convert.ToInt32(a);
            Comment comment = new Comment()
            {
                NameUser = name,
                Content = review,
                Email = email,
                ProductID = id,
            };

            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("ProductDetails", new { id = id });
        }
        // GET: Admin/Users/Edit/5
        public ActionResult UserInformation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserInformation([Bind(Include = "UserID,FullName,Email,PassWord,Address,Phone,RoleID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                Session["fullName"] = user.FullName;
                Session["address"] = user.Address;
                Session["phone"] = user.Phone;
                return RedirectToAction("UserInformation", new { id = Session["UserID"] });
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }
    }
}