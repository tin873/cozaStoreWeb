﻿using cozaStore.BusinessLogicLayer;
using cozaStore.Common;
using cozaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace cozaStore.Presentation.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductServices _product;
        private readonly ICategoryServices _category;
        private readonly ICommentServices _comment;
        private readonly IProductDetailServices _productDetail;
        private readonly IPromotion _promotion;
        public ProductsController(IProductServices product, ICategoryServices category, ICommentServices comment, IProductDetailServices productDetail, IPromotion promotion)
        {
            _product = product;
            _category = category;
            _comment = comment;
            _productDetail = productDetail;
            _promotion = promotion;
        }
        // GET: Products
        /// <summary>
        /// display product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <param name="page"></param>
        /// <param name="CurrentFilter"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(string id, FormCollection data, int? page, string CurrentFilter)
        {
            //delete promotion with end date <= DateTime.Now
            var promotions = _promotion.GetAll();
            foreach (var item in promotions)
            {
                if (item.EndDate <= DateTime.Now)
                {
                    _promotion.Delete(item.PromotionId);
                }
            }
            IEnumerable<Product> products;
            string search = data["search"];
            string searchHeader = data["searchHeader"];
            if (search != null || searchHeader != null)
            {
                page = 1;
            }
            else
            {
                search = CurrentFilter;
                searchHeader = CurrentFilter;
            }
            ViewData["CurrentFilter"] = search;
            ViewData["CurrentFilter"] = searchHeader;
            Expression<Func<Product, bool>> filter = null;
            if (search.IsNotBlank())
            {
                filter = b => b.ProductName.ToLower().Contains(search.ToLower());
            }
            if (searchHeader.IsNotBlank())
            {
                filter = b => b.ProductName.ToLower().Contains(searchHeader.ToLower());
            }
            if (id.IsBlank())
            {
                products = await _product.GetAsync(filter: filter,orderBy: p => p.OrderBy(x => x.ProductName),page: page?? 1, pageSize: 12);
            }
            else
            {
                products = await _product.GetAsync(filter: b => b.Category.CategoryID.ToString() == id, orderBy: p => p.OrderBy(x => x.ProductName), page: page??1, pageSize: 12);
            }
            return View(products);
        }

        /// <summary>
        /// display detail product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Detail(int id)
        {
            var product = await _product.GetByIdAsync(id);
            //get size
            List<SizeViewData> sizes = new List<SizeViewData>();
           
            var size = from s in product.ProductDetails
                       group s by new { s.Size } into g
                       select new {g.Key.Size};
            foreach (var item in size)
            {
                var aritem = item.ToString().Split(' ');
                var s = new SizeViewData() { Size = aritem[3] };
                sizes.Add(s);
            }
            ViewData["Size"] = sizes;
            //getcolor
            List<ColorViewData> colors = new List<ColorViewData>();
            var color = from s in product.ProductDetails
                       group s by new { s.Color } into g
                       select new { g.Key.Color };
            foreach (var item in color)
            {
                var aritem = item.ToString().Split(' ');
                var c = new ColorViewData() { Color = aritem[3] };
                colors.Add(c);
            }
            ViewData["Color"] = colors;
            //get image
            List<ImageViewData> images = new List<ImageViewData>();
            var image = from s in product.ProductDetails
                        group s by new { s.Image } into g
                        select new { g.Key.Image };
            foreach (var item in image)
            {
                var aritem = item.ToString().Split(' ');
                var i = new ImageViewData() { Image = aritem[3] };
                 images.Add(i);
            }
            ViewData["Image"] = images;
           
            if (product == null)
            {
                return HttpNotFound();
            }    
            return View(product);
        }

        /// <summary>
        /// display category partial
        /// </summary>
        /// <returns></returns>
        public PartialViewResult _Menu()
        {
            var categories =  _category.GetAll();
            return PartialView(categories);
        }

        /// <summary>
        /// display list size from color
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult _SelectSize(string id)
        {
            var arr = id.Split(' ');
            int productId = int.Parse(arr[0]);
            if(arr.Length < 2)
            {
                return PartialView();
            }    
            else
            {
                string color = arr[1];
                var product = _product.GetById(productId);
                var productDetail = product.ProductDetails.Where(x => x.Color.ToUpper().Equals(color.ToUpper()));
                //get size
                List<SizeViewData> sizes = new List<SizeViewData>();

                var size = from s in productDetail
                           group s by new { s.Size } into g
                           select new { g.Key.Size };
                foreach (var item in size)
                {
                    var aritem = item.ToString().Split(' ');
                    var s = new SizeViewData() { Size = aritem[3] };
                    sizes.Add(s);
                }
                return PartialView(sizes);
            }    
            
        }

        /// <summary>
        /// display realease product by category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult _RealeaseProduct(int id)
        {
            var products =  _product.FindAll(filter: x => x.Category.CategoryID == id);
            return PartialView(products);
        }

        /// <summary>
        /// set comment in table Comment of Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _Comment(FormCollection data)
        {
            var email = data["email"];
            var name = data["name"];
            var content = data["review"];
            var productID = data["idproduct"];
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
            var product = _product.GetById(id);
            var comment = new Comment()
            {
                Email = email,
                NameUser = name,
                Content = content,
                Product = product,
                CommentDate = DateTime.Now
            };
                _comment.Create(comment);
                return RedirectToAction("Detail", new { id = id });
        }
       
        /// <summary>
        /// not use
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult _Color(string id)
        {
            var arr = id.Split();
            int productId = int.Parse(arr[0]);
            string size = arr[1]; 
            var productDetails = _productDetail.FindAll(filter: x => x.ProductDetailId == productId && x.Size == size);
            List<ColorViewData> colors = new List<ColorViewData>();
            foreach (var item in productDetails)
            {
                var coloritem = item.Color;
                var color = new ColorViewData()
                {
                    Color = coloritem
                };
                colors.Add(color);
            }
            return PartialView(colors);
        }    
    }
}