using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cozaStore.Models
{
    [Table("Product")]
    public class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }


        [DisplayName("Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(50)]
        public string ProductName { get; set; }

        [DisplayName("Hình ảnh")]
        [Required(ErrorMessage = "Hình ảnh không được để trống")]
        [StringLength(100)]
        public string Image { get; set; }

        [StringLength(500)]
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [DisplayName("Giá")]
        [DisplayFormat(DataFormatString = "{0:0,0} vnđ")]
        public decimal Price { get; set; }

        [DisplayName("Kích cỡ")]
        [StringLength(15)]
        public string Size { get; set; }

        [DisplayName("Mầu sắc")]
        [StringLength(50)]
        public string Color { get; set; }

        [Required(ErrorMessage ="Số lượng sản phẩm không được để trống!")]
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        public int SupplierID { get; set; }

        public int CategoryID { get; set; }


        public virtual Category Category { get; set; }


        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
