using cozaStore.BusinessLogicLayer;
using cozaStore.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace cozaStore.Presentation.Areas.Admin.Controllers
{
    public class PromotionsManagerController : Controller
    {
        private readonly IPromotion _promotion;
        private readonly IProductServices _product;
        public PromotionsManagerController(IPromotion promotion, IProductServices product)
        {
            _promotion = promotion;
            _product = product;
        }

        // GET: Admin/PromotionsManager
        public async Task<ActionResult> Index()
        {
            var promotion = _promotion.GetAll();
            foreach (var item in promotion)
            {
                if (item.EndDate <= DateTime.Now)
                {
                    _promotion.Delete(item.PromotionId);
                }
            }
            var promotions = await _promotion.GetAllAsync();
            return View(promotions);
        }


        // GET: Admin/PromotionsManager/Create
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                var promotion = _promotion.GetById(id);
                ViewBag.error = "Sản phẩm " + promotion.Product.ProductName + " đang được giảm giá !";
            }
            ViewBag.PromotionId = new SelectList(_product.GetAll(), "ProductID", "ProductName");
            return View();
        }

        // POST: Admin/PromotionsManager/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PromotionId,Discount,StartDate,EndDate")] Promotion promotion)
        {
            var promotions = _promotion.GetById(promotion.PromotionId);
            if (promotions != null)
            {
                
                return RedirectToAction("Create", new { id = promotions.PromotionId});
            }   
            else
            {
                promotion.StartDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    await _promotion.CreateAsync(promotion);
                    return RedirectToAction("Index");
                }

                ViewBag.PromotionId = new SelectList(_product.GetAll(), "ProductID", "ProductName", promotion.PromotionId);
                return View(promotion);
            }    
        }


        // GET: Admin/PromotionsManager/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = await _promotion.GetByIdAsync(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // POST: Admin/PromotionsManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _promotion.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
