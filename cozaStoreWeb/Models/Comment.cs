namespace cozaStoreWeb.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Comment
    {
        public int CommentID { get; set; }

        [DisplayName("Tên người Comment")]
        [Required(ErrorMessage = "Tên người comment không được để trống")]
        [StringLength(50)]
        public string NameUser { get; set; }

        [DisplayName("Email")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Bạn cần nhập đúng định dạng mật khẩu")]
        [Required(ErrorMessage = "Email không được để trống")]
        [StringLength(50)]
        public string Email { get; set; }

        [DisplayName("Nội dung bình luận")]
        [Required(ErrorMessage = "Không được để trống nội dung bình luận")]
        [StringLength(500)]
        public string Content { get; set; }

        public int ProductID { get; set; }

        public virtual Product Product { get; set; }
    }
}
