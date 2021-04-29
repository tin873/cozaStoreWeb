using cozaStore.BusinessLogicLayer;
using cozaStore.Common;
using cozaStore.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace cozaStore.Presentation.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        private readonly IProductDetailServices _productDetail;
        private readonly IOrderDetailServices _orderDetail;
        private readonly IOrderServices _order;
        private readonly ICouponServices _coupon;
        private readonly ICheckOutServices _checkOut;
        private readonly IUserServieces _user;

        /// <summary>
        /// contructor CheckOutController
        /// </summary>
        /// <param name="product"></param>
        /// <param name="order"></param>
        /// <param name="orderDetail"></param>
        /// <param name="coupon"></param>
        /// <param name="checkOut"></param>
        /// <param name="user"></param>
        public CheckOutController(IProductDetailServices product, IOrderServices order, IOrderDetailServices orderDetail, ICouponServices coupon, ICheckOutServices checkOut, IUserServieces user)
        {
            _productDetail = product;
            _order = order;
            _orderDetail = orderDetail;
            _coupon = coupon;
            _checkOut = checkOut;
            _user = user;
        }

        /// <summary>
        /// Display list item of the cart
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(string couponCode)
        {
            //check and create couponcode
            if (couponCode != null)
            {
                Session[Constant.Code] = couponCode;
            }
            //check out cart have item
            if (Session[Constant.Code] != null)
            {
                //select Coupon Code with couponCode
                var coupon = _coupon.GetById(Session[Constant.Code]);
                //check coupon code Exist
                if (coupon != null)
                {
                    var cart = Session[Constant.Cart];
                    var list = new List<CartItem>();
                    if (cart != null)
                    {
                        list = (List<CartItem>)cart;
                    }
                    decimal total = 0m;
                    foreach (var item in list)
                    {
                        total += item.Total;
                    }
                    total -= (total * coupon.Discount) / 100;
                    ViewBag.GrandTotal = total.ToString("#,###");
                    return View(list);
                }
                else
                {
                    ViewBag.errormg = "Mã giảm giá đã hết hạn hoặc không tồn tại!";
                    var cart = Session[Constant.Cart];
                    var list = new List<CartItem>();
                    if (cart != null)
                    {
                        list = (List<CartItem>)cart;
                    }
                    decimal total = 0m;
                    foreach (var item in list)
                    {
                        total += item.Total;
                    }
                    ViewBag.GrandTotal = total.ToString("#,###");
                    return View(list);
                }
            }
            else
            {
                var cart = Session[Constant.Cart];
                var list = new List<CartItem>();
                if (cart != null)
                {
                    list = (List<CartItem>)cart;
                }
                decimal total = 0m;
                foreach (var item in list)
                {
                    total += item.Total;
                }
                ViewBag.GrandTotal = total.ToString("#,###");
                return View(list);
            }
        }

        /// <summary>
        /// get item in cart to check out
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(FormCollection data)
        {
            var cart = Session[Constant.Cart];
            var couponCode = Session[Constant.Code];
            var cartItems = (List<CartItem>)cart;
            var coupon = _coupon.GetById(couponCode);
            var fullName = data["fullName"];
            var phone = data["phone"];
            var address = data["address"];
            var description = data["description"];
            var userId = Session["userId"];
            User user = _user.GetById(userId);
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    ProductDetail = item.ProductDetail,
                    Quantity = item.Quantity
                };
                orderDetails.Add(orderDetail);
            }
            if(coupon == null)
            {
                coupon = _coupon.GetById("KHONGGIAMGIA");
            }    
            var order = new Order()
            {
                FullName = fullName,
                Address = address,
                Phone = phone,
                Description = description,
                Coupon = coupon,
                Status = Status.waitForConfirm,
                User = user
            };
            _checkOut.CheckOut(order, orderDetails);
            cartItems.Clear();
            Session[Constant.Code] = null;
            return RedirectToAction("CheckOutIsOk");
        }

        /// <summary>
        /// Display if check out success
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckOutIsOk()
        {
            return View();
        }
    }
}