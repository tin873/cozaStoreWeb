using cozaStore.BusinessLogicLayer;
using cozaStore.Common;
using cozaStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace cozaStore.Presentation.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductServices _product;
        public ShoppingCartController(IProductServices product)
        {
            _product = product;
        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = Session[Constant.Cart];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }
            decimal total = 0m;
            foreach (var item in list)
            {
                total += item.Total;
            }
            ViewBag.GrandTotal = total;
            return View(list);
        }
        public async Task<ActionResult> AddToCart(int id)
        {
            List<CartItem> cart = Session[Constant.Cart] as List<CartItem> ?? new List<CartItem>();

            CartItem model = new CartItem();

            Product product = await _product.GetByIdAsync(id);

            var itemInCart = cart.FirstOrDefault(x => x.Product.ProductID == id);

            if (itemInCart == null)
            {
                cart.Add(new CartItem()
                {
                    Product = product,
                    Quantity = 1,
                });
            }
            else
            {
                itemInCart.Quantity++;
            }

            int qty = 0;
            foreach (var item in cart)
            {
                qty += item.Quantity;
            }
            model.Quantity = qty;
            Session[Constant.Cart] = cart;

            return RedirectToAction("CartPartial");
        }
        public ActionResult CartPartial()
        {
            CartItem model = new CartItem();

            int qty = 0;
            if (Session[Constant.Cart] != null)
            {
                var list = (List<CartItem>)Session[Constant.Cart];
                foreach (var item in list)
                {
                    qty += item.Quantity;
                }
                model.Quantity = qty;
            }
            else
            {
                model.Quantity = 0;
            }

            return PartialView(model);
        }

        public JsonResult IncrementProduct(int productId)
        {
            List<CartItem> cart = Session[Constant.Cart] as List<CartItem>;

            CartItem item = cart.FirstOrDefault(x => x.Product.ProductID == productId);

            item.Quantity++;

            var result = new { qty = item.Quantity, price = item.Product.Price };

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult DecrementProduct(int productId)
        {
            List<CartItem> cart = Session[Constant.Cart] as List<CartItem>;

            CartItem item = cart.FirstOrDefault(x => x.Product.ProductID == productId);

            if (item.Quantity > 1)
            {
                item.Quantity--;
            }
            else
            {
                item.Quantity = 0;
                cart.Remove(item);
            }

            var result = new { qty = item.Quantity, price = item.Product.Price };

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public void RemoveProduct(int productId)
        {
            List<CartItem> cart = Session[Constant.Cart] as List<CartItem>;

            CartItem item = cart.FirstOrDefault(x => x.Product.ProductID == productId) ;

            cart.Remove(item);

        }
        public PartialViewResult CartMini()
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
            ViewBag.SumTotal = total;
            return PartialView(list);
        }
    }
}