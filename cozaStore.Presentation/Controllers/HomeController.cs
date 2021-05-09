using cozaStore.BusinessLogicLayer;
using cozaStore.Common;
using cozaStore.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace cozaStore.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly ICategoryServices _category;
        private readonly IContactServices _contact;
        private readonly IUserServieces _user;
        private readonly IRoleServices _role;
        private readonly IPromotion _promotion;

        /// <summary>
        /// Contructor of HomeController
        /// </summary>
        /// <param name="productServices"></param>
        /// <param name="category"></param>
        /// <param name="contact"></param>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <param name="promotion"></param>
        public HomeController(IProductServices productServices, ICategoryServices category, IContactServices contact, IUserServieces user, IRoleServices role, IPromotion promotion)
        {
            _productServices = productServices;
            _category = category;
            _contact = contact;
            _user = user;
            _role = role;
            _promotion = promotion;
        }

        /// <summary>
        /// Display Product 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var promotions = _promotion.GetAll();
            foreach (var item in promotions)
            {
                if (item.EndDate <= DateTime.Now)
                {
                    _promotion.Delete(item.PromotionId);
                }
            }
            var products = _productServices.GetTop(orderBy: x => x.OrderBy(p => p.ProductName));
            return View(products);
        }

        /// <summary>
        /// Display product with orderby Name
        /// </summary>
        /// <returns></returns>
        public PartialViewResult _TopProductNew()
        {
            var products = _productServices.GetTop(orderBy: x => x.OrderByDescending(p => p.ProductName));
            return PartialView(products);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// dishplay view contact
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        /// <summary>
        /// get information contact of custommer
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Contact(FormCollection data)
        {
            var email = data["email"];
            var msg = data["msg"];
            var contact = new Contact()
            {
                Email = email,
                Description = msg
            };
            await _contact.CreateAsync(contact);
            return View();
        }


        /// <summary>
        /// view login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Login(FormCollection data)
        {
            var email = data["email"];
            var password = data["pass"];
            var user = await _user.FindAsync(filter: x => x.Email.Equals(email) && x.PassWord.Equals(password));
            //check user Exist
            if (user != null)
            {
                //check role
                if (user.Role.RoleID == 2)
                {
                    Session["fullName"] = user.FullName;
                    Session["address"] = user.Address;
                    Session["phone"] = user.Phone;
                    Session["email"] = user.Email;
                    Session["userId"] = user.UserID;
                    if(Session[Constant.Cart] != null)
                    {
                        return RedirectToAction("Index","ShoppingCart");
                    }   
                    else
                    {
                        return RedirectToAction("Index");
                    }    
                }
                else
                {
                    Session["fullNameAdmin"] = user.FullName;
                    Session["addressAdmin"] = user.Address;
                    Session["emailAdmin"] = user.Email;
                    Session["phoneAdmin"] = user.Phone;
                    Session["adminId"] = user.UserID;
                    return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
                }
            }
            else
            {
                ViewBag.error = "Sai tên đăng nhập hoặc mật khẩu!";
            }
            return View();
        }

        /// <summary>
        /// view reigster
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// get infomation of customer insert to table User
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Register(FormCollection data)
        {
            var fullName = data["fullName"];
            var email = data["email"];
            var pass = data["pass"];
            var address = data["address"];
            var phone = data["phone"];
            var users = _user.Find(filter: x => x.Email.Equals(email));
            var users1 = _user.Find(filter: x => x.Phone.Equals(phone));
            var role = await _role.GetByIdAsync(2);
            //check email not Exit
            if (users == null)
            {
                //check phone not Exit
                if(users1 == null)
                {
                    var user = new User()
                    {
                        FullName = fullName,
                        Email = email,
                        PassWord = pass,
                        Address = address,
                        Phone = phone,
                        Role = role
                    };
                    await _user.CreateAsync(user);
                    return RedirectToAction("Login");
                }    
                else
                {
                    ViewBag.error2 = "Số điện thoại này đã được đăng kí!";
                }    
            }
            else
            {
                ViewBag.error1 = "Email này đã tồn tại!";
            }
            return View();
        }

        /// <summary>
        /// display information User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> InforUser()
        {
            var id = Session["userId"];
            var user = await _user.GetByIdAsync(id);
            return View(user);
        }

        /// <summary>
        /// Edit information User
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> InforUser(FormCollection data)
        {
            var id = Session["userId"];
            var fullName = data["fullName"];
            var email = data["email"];
            var pass = data["pass"];
            var address = data["address"];
            var phone = data["phone"];
            var user = new User()
            {
                UserID = int.Parse(id.ToString()),
                FullName = fullName,
                Email = email,
                PassWord = pass,
                Address = address,
                Phone = phone,
                RoleID = 2
            };
            if(user != null)
            {
                await _user.UpdateAsync(user);
                Session["fullName"] = user.FullName;
                Session["address"] = user.Address;
                Session["phone"] = user.Phone;
                Session["email"] = user.Email;
                return RedirectToAction("Index");
            }    
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        public PartialViewResult _Footer()
        {
            var categories = _category.GetAll();
            return PartialView(categories);
        }
    }
}