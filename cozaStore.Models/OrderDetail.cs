using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozaStore.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        public int ProductID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int OrderID { get; set; }

        [DisplayName("số lượng")]
        [RegularExpression(@"[0-9]+$", ErrorMessage = "Bạn cần nhập số")]
        [Required(ErrorMessage = "Email không được để trống")]
        [StringLength(30)]
        public string Quantity { get; set; }



        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
