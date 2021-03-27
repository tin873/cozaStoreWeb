using cozaStore.BusinessLogicLayer;
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
        public ProductsController(IProductServices product, ICategoryServices category)
        {
            _product = product;
            _category = category;
        }
        // GET: Products
        public async Task<ActionResult> Index(string id, FormCollection data, int? page, string CurrentFilter)
        {
            IEnumerable<Product> products;
            string search = data["search"];
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = CurrentFilter;
            }
            ViewData["CurrentFilter"] = search;
            Expression<Func<Product, bool>> filter = null;
            if (search.IsNotBlank())
            {
                filter = b => b.ProductName.ToLower().Contains(search.ToLower());
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

        public PartialViewResult _Menu()
        {
            var categories =  _category.GetAll();
            return PartialView(categories);
        }
    }
}