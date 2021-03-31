using cozaStore.Models;

namespace cozaStore.Presentation
{
    public class CartItem
    {
        public int Quantity { get; set; }

        public Product Product { get; set; }

        public decimal Total { get { return Product.Price * Quantity; } }

        
    }
}
