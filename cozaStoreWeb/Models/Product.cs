namespace cozaStoreWeb.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Comments = new HashSet<Comment>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductID { get; set; }

        public int SupplierID { get; set; }

        public int CategoryID { get; set; }

        [DisplayName("Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(30)]
        public string ProductName { get; set; }

        [DisplayName("Hình ảnh")]
        [Required(ErrorMessage = "Hình ảnh không được để trống")]
        [StringLength(100)]
        public string Image { get; set; }

        [StringLength(500)]
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [DisplayName("Giá")]
        [DisplayFormat(DataFormatString ="{0:0,0} vnđ")]
        public decimal Price { get; set; }
        public int? SizeID { get; set; }

        public int? ColorID { get; set; }

        public virtual Category Category { get; set; }

        public virtual Color Color { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Size Size { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
