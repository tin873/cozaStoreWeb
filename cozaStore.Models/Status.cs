using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cozaStore.Models
{
    [Table("Status")]
    public class Status
    {
        [Key]
        public int StatusId { get; set; }

        [DisplayName("Tên trạng thái")]
        [Required(ErrorMessage = "tên trạng thái không được để trống")]
        [StringLength(30)]
        public string StatusName { get; set; }

        [DisplayName("Mô tả")]
        [StringLength(500)]
        public string Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
