using cozaStoreWeb.Dao;
using cozaStoreWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace cozaStoreWeb.Controllers
{
    public class CartController : Controller
    {
        cozaStoreDB db = new cozaStoreDB();
        private const string CartSession = "CartSession";
        // GET: Cart
        [HttpGet]
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Index(FormCollection data)
        {
            var address = data["address"];
            var phone = data["phone"];
            var use = db.Users.Find(Session["UserID"]);
            var order = new Order()
            {
                OrderDate = DateTime.Now,
                UserID = use.UserID,
                Address = address,
                Phone = phone,
                Status = false,
            };
            int id = new OrderDao().Insert(order);
            var detailDao = new OrderDetailsDao();
            var cart = (List<CartItem>)Session[CartSession];
            foreach (var item in cart)
            {
                var orderDetails = new OrderDetail()
                {
                    ProductID = item.Product.ProductID,
                    OrderID = id,
                    Quantity = item.Quantity.ToString(),
                    Size = item.Size,
                    Color = item.Color,
                };
                detailDao.Insert(orderDetails);
            }
            cart.Clear();
            return RedirectToAction("OrderYes");
        }
        public PartialViewResult _Cart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
        //public ActionResult UpdateItem(List<CartItem> listItem)
        //{
        //    listItem.ToArray();
        //    var cart = Session[CartSession];
        //    var list = (List<CartItem>)cart;
        //    foreach (var item in list)
        //    {
        //        for (int i = 0; i < listItem.Count; i++)
        //        {
        //            if(item.Product.ProductID == listItem[i].Product.ProductID)
        //            {
        //                item.Quantity = listItem[i].Quantity;
        //            }    
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ProductID == item.Product.ProductID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new { 
             status = true           
            });
        }

        [HttpPost]
        public ActionResult AddItem(FormCollection data)
        {
            int productID = int.Parse(data["productID"]);
            int quantity = int.Parse(data["quantity"]);
            string color = data["color"];
            string size = data["size"];
            var product = db.Products.Find(productID);
            var idUser = Session["UserID"];
            var user = db.Users.Find(idUser);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ProductID == productID))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ProductID == productID)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    item.Size = size;
                    item.Color = color;
                    item.User = user;
                    list.Add(item);

                }
                Session[CartSession] = list;
            }
            else
            {

                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                item.Size = size;
                item.Color = color;
                item.User = user;
                var list = new List<CartItem>();
                list.Add(item);

                Session[CartSession] = list;

            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            list = (List<CartItem>)cart;
            list.RemoveAll(x => x.Product.ProductID == id);
            return RedirectToAction("Index");
        }
        public ActionResult Delete1(int id)
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            list = (List<CartItem>)cart;
            list.RemoveAll(x => x.Product.ProductID == id);
            return RedirectToAction("Index", "Home");
        }
        public PartialViewResult _HeaderCart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
        public ActionResult OrderYes()
        {
            return View();
        }
    }
}