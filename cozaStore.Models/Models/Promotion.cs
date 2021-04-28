using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozaStore.Models
{
    [Table("Promotion")]
    public class Promotion
    {
        public int PromotionId { get; set; }

        public int Discount { get; set; }

        public DateTime EndDate { get; set; }

        public StatusOfPromotion StatusOfPromotion { get; set; }


    }
}
