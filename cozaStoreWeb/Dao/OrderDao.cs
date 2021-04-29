using cozaStoreWeb.Models;

namespace cozaStoreWeb.Dao
{
    public class OrderDao
    {
        cozaStoreDB db = null;
        public OrderDao()
        {
            db = new cozaStoreDB();
        }
        public int Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.OrderID;
        }
    }
}