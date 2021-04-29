namespace cozaStoreWeb.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [DisplayName("số lượng")]
        [RegularExpression(@"[0-9]+$", ErrorMessage = "Bạn cần nhập số")]
        [Required(ErrorMessage = "Email không được để trống")]
        [StringLength(30)]
        public string Quantity { get; set; }

        [StringLength(30)]
        [DisplayName("Mầu sắc")]
        public string Color { get; set; }

        [StringLength(15)]
        [DisplayName("Kích cỡ")]
        public string Size { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
