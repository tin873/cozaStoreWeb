using cozaStore.BusinessLogicLayer;
using cozaStore.Common;
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
        public CheckOutController(IProductServices product, IOrderServices order, IOrderDetailServices orderDetail, ICouponServices coupon)
        {
            _product = product;
            _order = order;
            _orderDetail = orderDetail;
            _coupon = coupon;
        }
        public ActionResult Index(string couponCode)
        {
            if(couponCode != null)
            {
                Session[Constant.Code] = couponCode;
            }    
            if(Session[Constant.Code] != null)
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

    }
}