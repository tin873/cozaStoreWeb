using cozaStore.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace cozaStore.DataAccessLayer
{
    public class DbInitializer : CreateDatabaseIfNotExists<cozaStoreDbContext>
    {
        protected override void Seed(cozaStoreDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            #region Add Category
            var categories = new List<Category>()
            {
                new Category()
                {
                    CategoryName = "Đồ Nam",
                    Description = "Quần áo nam chất liệu tốt thời thượng."
                },
                new Category()
                {
                    CategoryName = "Đồ Nữ",
                    Description = "Quần áo nữ chất liệu tốt thời thượng."
                },
                new Category()
                {
                    CategoryName = "Đồ Đôi",
                    Description = "Quần áo đôi trẻ trung năng động."
                },
                new Category()
                {
                    CategoryName = "Áo Khoác",
                    Description = "Quần áo chất liệu đẹp không lo bị lạnh."
                }
            };
            context.Categories.AddRange(categories);
            #endregion

            #region Add Role
            var roles = new List<Role>()
            {
                new Role()
                {
                    RoleID = 1,
                    RoleName = "Admin"
                },
                new Role()
                {
                    RoleID = 2,
                    RoleName = "User"
                }
            };
            context.Roles.AddRange(roles);
            #endregion

            #region Add Coupon
            var coupons = new List<Coupon>()
            {
                new Coupon()
                {
                    CouponCode = "TETDENROI",
                    Discount = 20,
                    Description ="Giảm giá nhân dịp tết"
                },
                new Coupon()
                {
                    CouponCode = "SINHNHAT",
                    Discount = 10,
                    Description ="Giảm giá nhân dịp sinh nhật"
                },
            };
            context.Coupons.AddRange(coupons);
            #endregion

            #region Add Status
            var statuses = new List<Status>()
            {
                new Status()
                {
                    StatusId = 1,
                    StatusName = "Đã đặt",
                    Description = "Hàng đã được đặt!"
                },
                new Status()
                {
                    StatusId = 2,
                    StatusName = "Đang vận chuyển",
                    Description = "Hàng đang được vận chuyển!"
                },
                 new Status()
                {
                    StatusId = 3,
                    StatusName = "Đã nhận hàng",
                    Description = "Hàng đã được nhận bởi khách hàng!"
                },
                  new Status()
                {
                    StatusId = 4,
                    StatusName = "Đã hủy",
                    Description = "Hàng đã được hủy!"
                }
            };
            context.Statuses.AddRange(statuses);
            #endregion

            #region Add Supplier
            var suppliers = new List<Supplier>()
            {
                new Supplier()
                {
                    SupplierName = "Nguyễn Duy",
                    Address = "Hà Nội",
                    Phone = "0367641095"
                },
                new Supplier()
                {
                    SupplierName = "Phan Thị",
                    Address = "Hà Nam",
                    Phone = "0914300231"
                },
                new Supplier()
                {
                    SupplierName = "Hải Nam",
                    Address = "Hà Đông",
                    Phone = "0367641065"
                },
                new Supplier()
                {
                    SupplierName = "Hùng Cường",
                    Address = "Bắc Giang",
                    Phone = "0984884182"
                },
                new Supplier()
                {
                    SupplierName = "Nhuận Phát",
                    Address = "Hà Tĩnh",
                    Phone = "0393219136"
                },
                new Supplier()
                {
                    SupplierName = "Bá Khiêm",
                    Address = "Bắc Giang",
                    Phone = "0376548935"
                }
            };
            context.Suppliers.AddRange(suppliers);
            #endregion

            #region Add User
            var users = new List<User>()
            {
                new User()
                {
                    FullName = "Nguyễn Duy Tín",
                    Email = "tinduy@gmail.com",
                    PassWord = "tin123",
                    Address = "Hà Nội",
                    Phone = "0367641095",
                    Role = roles.FirstOrDefault(r => r.RoleID ==1)
                },
                new User()
                {
                    FullName = "Nguyễn Thị Hà",
                    Email = "hanguyen@gmail.com",
                    PassWord = "ha123",
                    Address = "Hà Nội",
                    Phone = "0674693659",
                    Role = roles.FirstOrDefault(r => r.RoleID ==1)
                },
                new User()
                {
                    FullName = "Nguyễn Thị Thu",
                    Email = "thu@gmail.com",
                    PassWord = "thu123",
                    Address = "Hà Nội",
                    Phone = "0914300231",
                    Role = roles.FirstOrDefault(r => r.RoleID ==2)
                },
                new User()
                {
                    FullName = "Lương Đình Nam",
                    Email = "namluong@gmail.com",
                    PassWord = "nam123",
                    Address = "Vĩnh Phúc",
                    Phone = "0367641096",
                    Role = roles.FirstOrDefault(r => r.RoleID ==2)
                }
            };
            context.Users.AddRange(users);
            #endregion

            #region Add Product
            var products = new List<Product>()
            {
                new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Nguyễn Duy")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Nam")),
                    ProductName = "Áo somi M2SMN3",
                    Image = "aococnam1.jpg",
                    Description = "Áo sơ mi chất liệu đẹp tôn dáng người mặc thoải mái mát mẻ mùa hè phù hợp với việc mặc đi chơi dã ngoại...",
                    Price = 250000,
                    Size = "L",
                    Color = "Màu xanh da trời",
                    Quantity = 50
                },
                new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Nguyễn Duy")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Nam")),
                    ProductName = "Áo somi M2SMN5",
                    Image = "aococnam2.jpg",
                    Description = "Áo sơ mi chất liệu đẹp tôn dáng người mặc thoải mái mát mẻ mùa hè phù hợp với việc mặc đi chơi dã ngoại...",
                    Price = 250000,
                    Size = "XL",
                    Color = "Màu xanh nước biển",
                    Quantity = 30
                },
                new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Nguyễn Duy")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Nam")),
                    ProductName = "Áo Phông M2SPN2",
                    Image = "aophongnam1.jpg",
                    Description = "Áo phông chất liệu tốt mát mẻ cho mùa hè, thoáng khí giảm đi sự tự ti trong bạn.",
                    Price = 300000,
                    Size = "M",
                    Color = "Màu cam",
                    Quantity = 50
                },
                new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Nguyễn Duy")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Nam")),
                    ProductName = "Áo Phông M2SPN1",
                    Image = "aophongnam2.jpg",
                    Description = "Áo phông chất liệu tốt mát mẻ cho mùa hè, thoáng khí giảm đi sự tự ti trong bạn.",
                    Price = 180000,
                    Size = "Free size",
                    Color = "Màu trắng",
                    Quantity = 30
                },
                new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Nguyễn Duy")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Nam")),
                    ProductName = "Áo somi M2SMN9",
                    Image = "aosomiNam1.jpg",
                    Description = "Áo sơ mi dài tay tạo nên sự thanh lịch và quý phái khi khoác lên mình còn chần chờ gì nữa đặt mua ngay thôi!",
                    Price = 300000,
                    Size = "2XL",
                    Color = "Màu đen",
                    Quantity = 10
                },
                new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Nguyễn Duy")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Nam")),
                    ProductName = "Áo somi M2SMSM2",
                    Image = "sominam2.jpg",
                    Description = "Áo sơ mi dài tay tạo nên sự thanh lịch và quý phái khi khoác lên mình còn chần chờ gì nữa đặt mua ngay thôi!",
                    Price = 255000,
                    Size = "XL",
                    Color = "Màu Nâu",
                    Quantity = 16
                },
                new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Nguyễn Duy")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Nam")),
                    ProductName = "Quần jean M2SMJ8",
                    Image = "quanjeannam1.jpg",
                    Description = "Sản phẩm quần jean là một sản phẩm có chất liệu tốt có thể phối cùng chiếc áo phông để giúp bạn trông bụi hơn hoặc có thể đi kèm 1 chiếc áo sơ mi để tạo lên sự thanh lịch.",
                    Price = 325000,
                    Size = "30",
                    Color = "Xanh nước biển",
                    Quantity = 12
                },
                new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Nguyễn Duy")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Nam")),
                    ProductName = "Quần jean M2SMJ2",
                    Image = "quanjeannam2.jpg",
                    Description = "Sản phẩm quần jean là một sản phẩm có chất liệu tốt có thể phối cùng chiếc áo phông để giúp bạn trông bụi hơn hoặc có thể đi kèm 1 chiếc áo sơ mi để tạo lên sự thanh lịch.",
                    Price = 285000,
                    Size = "29",
                    Color = "Xanh nước biển",
                    Quantity = 5
                },
                new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Nguyễn Duy")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Nam")),
                    ProductName = "Quần jean M2SMJ6",
                    Image = "quanjeannam3.jpg",
                    Description = "Sản phẩm quần jean là một sản phẩm có chất liệu tốt có thể phối cùng chiếc áo phông để giúp bạn trông bụi hơn hoặc có thể đi kèm 1 chiếc áo sơ mi để tạo lên sự thanh lịch.",
                    Price = 325000,
                    Size = "32",
                    Color = "Xanh nước biển",
                    Quantity = 20
                },
                new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Phan Thị")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Nữ")),
                    ProductName = "Áo phông MN2SM3",
                    Image = "aococnu1.jpg",
                    Description = "Áo phông chất liệu đẹp tôn dáng người mặc thoải mái mát mẻ mùa hè có thể mặc đi chơi dã ngoại...",
                    Price = 180000,
                    Size = "L",
                    Color = "Màu Vàng",
                    Quantity = 42
                },
                 new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Phan Thị")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Nữ")),
                    ProductName = "Áo tay dài M2SDN2",
                    Image = "aodainu1.jpg",
                    Description = "Áo dài tay dành cho nữ kiểu dáng thời thượng đang là xu thế trên thị trường và được nhiều bạn nữ săn đón có rất nhiều mầu sắc khác nhau..",
                    Price = 300000,
                    Size = "XL",
                    Color = "Màu Trắng",
                    Quantity = 32
                },
                  new Product()
                {
                     Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Phan Thị")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Nữ")),
                    ProductName = "Chân váy M2SCV3",
                    Image = "chanvaynu.jpg",
                    Description = "Chân váy M2SCV3 đang là 1 mẫu hot trên thị trường và được rất nhiều bạn trẻ săn đón rất phù hợp khi đi built cùng 1 chiếc áo sơ mi trắng!",
                    Price = 225000,
                    Size = "29",
                    Color = "Màu đen",
                    Quantity = 23
                },
                   new Product()
                {
                     Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Phan Thị")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Nữ")),
                    ProductName = "Quần bò NUDE23",
                    Image = "quanbonu1.jpg",
                    Description = "Quần bò chất liệu vải bò bền bỉ không những thế chiếc quần còn rất hợp thời trang thể hiện sự cá tính trong con người bạn.",
                    Price = 300000,
                    Size = "30",
                    Color = "Màu xanh nước biển",
                    Quantity = 50
                },
                    new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Phan Thị")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Nữ")),
                    ProductName = "Quần bò NUDE28",
                    Image = "quanbonu2.jpg",
                    Description = "Quần bò chất liệu vải bò bền bỉ không những thế chiếc quần còn rất hợp thời trang thể hiện sự cá tính trong con người bạn.",
                    Price = 250000,
                    Size = "31",
                    Color = "Màu xanh nước biển",
                    Quantity = 32
                },
                    new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Hùng Cường")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Đôi")),
                    ProductName = "Áo Ðôi M2SDG7",
                    Image = "aokhoacdoi1.jpg",
                    Description = "Áo khoác đôi rất ấm cho mùa đông không còn sợ cô đơn việc tránh rét không thể dễ hơn!",
                    Price = 800000,
                    Size = "Free size",
                    Color = "Màu đen",
                    Quantity = 53
                },
                    new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Hùng Cường")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Đồ Đôi")),
                    ProductName = "Áo Ðôi M2SMD3",
                    Image = "aophongdoi1.jpg",
                    Description = "Áo phông đôi mặc thoải mái phù hợp cho các bạn trẻ!",
                    Price = 350000,
                    Size = "Free size",
                    Color = "Màu trắng",
                    Quantity = 50
                },
                     new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Hùng Cường")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Áo Khoác")),
                    ProductName = "Áo Khoác M2SDG2",
                    Image = "aokhoacbo1.jpg",
                    Description = "Áo khoác chắn gió giúp đi đường cản gió tránh gió không lo bị lạnh mặc là ấm...",
                    Price = 700000,
                    Size = "Free Size",
                    Color = "Màu đen",
                    Quantity = 5
                },
                         new Product()
                {
                    Supplier = suppliers.FirstOrDefault(s => s.SupplierName.Equals("Nguyễn Duy")),
                    Category = categories.FirstOrDefault(c => c.CategoryName.Equals("Áo Khoác")),
                    ProductName = "Áo Khoác M2SDG1",
                    Image = "aokhoacbo2.jpg",
                    Description = "Áo khoác chắn gió giúp đi đường cản gió tránh gió không lo bị lạnh mặc là ấm...",
                    Price = 650000,
                    Size = "free size",
                    Color = "Màu đen",
                    Quantity = 0
                }
            };
            context.Products.AddRange(products);
            #endregion
            context.SaveChanges();
        }
    }
}
