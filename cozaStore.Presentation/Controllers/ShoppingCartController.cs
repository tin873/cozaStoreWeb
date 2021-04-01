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
        private readonly IProductDetailServices _productDetail;
        public ShoppingCartController(IProductServices product, IProductDetailServices productDetail)
        {
            _product = product;
            _productDetail = productDetail;
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
            ViewBag.GrandTotal = total.ToString("#,###");
            return View(list);
        }

        public async Task<ActionResult> GetIdProductDetail(string id)
        {
            var arr = id.Split(' ');
            int productId = int.Parse(arr[0]);
            string color = arr[1];
            string size = arr[2];
            Product product = await _product.GetByIdAsync(productId);
            ProductDetail productDetail = product.ProductDetails.Where(x => x.Color.ToUpper().Contains(color.ToUpper())&& x.Size.ToUpper().Contains(size.ToUpper())).FirstOrDefault();
            int id1 = productDetail.ProductDetailId;
            return RedirectToAction("AddToCart", new { id = id1 });
        }
        public async Task<ActionResult> AddToCart(int id)
        {
            List<CartItem> cart = Session[Constant.Cart] as List<CartItem> ?? new List<CartItem>();

            CartItem model = new CartItem();

            ProductDetail product = await _productDetail.GetByIdAsync(id);

            var itemInCart = cart.FirstOrDefault(x => x.ProductDetail.ProductDetailId == id);

            if (itemInCart == null)
            {
                cart.Add(new CartItem()
                {
                    ProductDetail = product,
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

            CartItem item = cart.FirstOrDefault(x => x.ProductDetail.ProductDetailId == productId);

            item.Quantity++;

            var result = new { qty = item.Quantity, price = item.ProductDetail.Price };

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult DecrementProduct(int productId)
        {
            List<CartItem> cart = Session[Constant.Cart] as List<CartItem>;

            CartItem item = cart.FirstOrDefault(x => x.ProductDetail.ProductDetailId == productId);

            if (item.Quantity > 1)
            {
                item.Quantity--;
            }
            else
            {
                item.Quantity = 0;
                cart.Remove(item);
            }

            var result = new { qty = item.Quantity, price = item.ProductDetail.Price };

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public void RemoveProduct(int productId)
        {
            List<CartItem> cart = Session[Constant.Cart] as List<CartItem>;

            CartItem item = cart.FirstOrDefault(x => x.ProductDetail.ProductDetailId == productId) ;

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