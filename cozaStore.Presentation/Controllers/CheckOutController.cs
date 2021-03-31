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
        private readonly IProductServices _product;
        private readonly IOrderDetailServices _orderDetail;
        private readonly IOrderServices _order;
        private readonly ICouponServices _coupon;
        private readonly ICheckOutServices _checkOut;
        private readonly IStatusServices _status;
        public CheckOutController(IProductServices product, IOrderServices order, IOrderDetailServices orderDetail, ICouponServices coupon, ICheckOutServices checkOut, IStatusServices status)
        {
            _product = product;
            _order = order;
            _orderDetail = orderDetail;
            _coupon = coupon;
            _checkOut = checkOut;
            _status = status;
        }
        [HttpGet]
        public ActionResult Index(string couponCode)
        {
            if (couponCode != null)
            {
                Session[Constant.Code] = couponCode;
            }
            if (Session[Constant.Code] != null)
            {
                var coupon = _coupon.GetById(Session[Constant.Code]);
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
            var status = _status.GetById(1);
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Product = item.Product,
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
                Status = status
            };
            _checkOut.CheckOut(order, orderDetails);
            cartItems.Clear();
            Session[Constant.Code] = null;
            return RedirectToAction("CheckOutIsOk");
        }

        public ActionResult CheckOutIsOk()
        {
            return View();
        }
    }
}