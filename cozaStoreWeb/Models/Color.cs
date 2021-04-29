namespace cozaStoreWeb.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Color
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Color()
        {
            Products = new HashSet<Product>();
        }

        public int ColorID { get; set; }

        [DisplayName("Tên mầu sắc")]
        [Required(ErrorMessage = "Mầu sắc không được để trống")]
        [StringLength(30)]
        public string ColorName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
