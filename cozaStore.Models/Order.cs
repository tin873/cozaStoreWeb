using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cozaStore.Models
{
    [Table("Order")]
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [DisplayName("Ngày Đặt hàng")]
        public DateTime CreateDate { get; set; }

        [DisplayName("Ngày gửi")]
        public DateTime ShippedDate { get; set; }
        public int UserID { get; set; }

        [DisplayName("Họ tên")]
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(30)]
        public string FullName { get; set; }

        [DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [StringLength(50, MinimumLength =10, ErrorMessage ="Địa chỉ có ít nhất 10 kí tự và nhiều nhất 50 kí tự.")]
        public string Address { get; set; }

        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [StringLength(20)]
        public string Phone { get; set; }

        public int StatusId { get; set; }

        public virtual Status Status { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
