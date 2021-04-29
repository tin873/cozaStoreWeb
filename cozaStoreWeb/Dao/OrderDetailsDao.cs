using cozaStoreWeb.Models;

namespace cozaStoreWeb.Dao
{
    public class OrderDetailsDao
    {
        cozaStoreDB db = null;
        public OrderDetailsDao()
        {
            db = new cozaStoreDB();
        }
        public bool Insert(OrderDetail detail)
        {
            db.OrderDetails.Add(detail);
            db.SaveChanges();
            return true;
        }
    }
}