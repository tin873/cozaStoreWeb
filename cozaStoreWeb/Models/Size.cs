namespace cozaStoreWeb.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Size
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Size()
        {
            Products = new HashSet<Product>();
        }

        public int SizeID { get; set; }

        [DisplayName("Kích cỡ")]
        [Required(ErrorMessage = "Kích cỡ không được để trống")]
        [StringLength(15)]
        public string SizeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
