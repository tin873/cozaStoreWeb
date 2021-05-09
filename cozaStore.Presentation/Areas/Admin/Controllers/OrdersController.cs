using cozaStore.BusinessLogicLayer;
using cozaStore.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace cozaStore.Presentation.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderServices _order;

        public OrdersController(IOrderServices order)
        {
            _order = order;
        }
        // GET: Admin/Orders
        public async Task<ActionResult> Index()
        {
            var orders = await _order.GetAllAsync();
            return View(orders);
        }

        public void SubmitOrder(int orderId)
        {
            var order =  _order.GetById(orderId);
            order.Status = Status.shipping;
             _order.Update(order);
        }

        public async Task<ActionResult> OrderDetail(int orderId)
        {
            var order = await _order.GetByIdAsync(orderId);
            return View(order);
        }
    }
}
