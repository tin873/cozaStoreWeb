using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace cozaStore.Models
{
    [Table("Promotion")]
    public class Promotion
    {
        public int PromotionId { get; set; }

        [DisplayName("Phần trăm giảm")]
        public int Discount { get; set; }

        [DisplayName("Ngày bắt đầu")]
        public DateTime StartDate { get; set; }

        [DisplayName("Ngày kết thúc")]
        public DateTime EndDate { get; set; }

        public virtual Product Product { get; set; }
    }
}
