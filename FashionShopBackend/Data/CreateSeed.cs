using FashionShopBackend.Model;
using FashionShopNETCoreAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace FashionShopBackend.Data
{
    public class CreateSeed
    {
        private readonly FashionShopDBContext context;
        public CreateSeed(FashionShopDBContext fashionShopDBContext)
        {
            this.context = fashionShopDBContext;
        }
        public void Seed()
        {
            ////categories
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(new List<Category>()
                    {
                        new Category()
                        {
                            category_name = "đầm",
                            category_image = "https://dam.jpg",
                            category_description = "đầm là trang phục liền mảnh vừa che phần trên cơ thể và phần dưới thắt lưng. ",
                        }, new Category()
                        {
                            category_name = "áo thun",
                            category_image = "https://aothun.jpg",
                            category_description = "áo thun là loại áo được làm từ vải bông, khác với áo sơ mi được làm từ vải dệt kim hoặc vải thoi.",
                        }, new Category()
                        {
                            category_name = "áo sơ mi",
                            category_image = "https://aosomi.jpg",
                            category_description = "áo sơ-mi là loại hàng may mặc bao bọc lấy thân mình và hai cánh tay của cơ thể.",
                        }
                    });
                context.SaveChanges();
            }
            ////products
            if (!context.Products.Any())
            {
                context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            product_name = "đầm cut-out ngắn tay",
                            product_image = "https://product.hstatic.net/200000584505/product/_mg_9359-edit_bc600f82516f4391ba4ab3880806ffb6_large.jpg",
                            product_description = "đầm cut-out ngắn tay",
                            product_price = 959000,
                            product_promotion = 15,
                            category_id = 1
                        },new Product()
                        {
                            product_name = "đầm kiểu tay phồng",
                            product_image = "https://product.hstatic.net/200000584505/product/img_6566-edit_b9cfa1d73d904b41b56bdbdaf49eb3ba_large.jpg",
                            product_description = "đầm kiểu tay phồng",
                            product_price = 729000,
                            product_promotion = 10,
                            category_id = 1
                        },new Product()
                        {
                            product_name = "đầm form rộng kiểu ngắn tay",
                            product_image = "https://product.hstatic.net/200000584505/product/img_6560-edit_3809133f1e324f2ba3f026492f9b897d_large.jpg",
                            product_description = "đầm kiểu tay phồng",
                            product_price = 729000,
                            product_promotion = 10,
                            category_id = 1
                        },new Product()
                        {
                            product_name = "áo thun croptop ngắn tay",
                            product_image = "https://product.hstatic.net/200000584505/product/_mg_1218-edit_8d67ac69d9e64742bab6867ddd58b43f_large.jpg",
                            product_description = "áo thun croptop ngắn tay",
                            product_price = 249000,
                            product_promotion = 14,
                            category_id = 2
                        },new Product()
                        {
                            product_name = "áo thun in chữ ngắn tay",
                            product_image = "https://product.hstatic.net/200000584505/product/_mg_7984-edit_f7fb45614cf64b758caf2643f3aa3207_large.jpg",
                            product_description = "áo thun in chữ ngắn tay",
                            product_price = 349000,
                            product_promotion = 7,
                            category_id = 2
                        },new Product()
                        {
                            product_name = "áo thun dài tay",
                            product_image = "https://product.hstatic.net/200000584505/product/_mg_7984-edit_f7fb45614cf64b758caf2643f3aa3207_large.jpg",
                            product_description = "áo thun dài tay",
                            product_price = 420000,
                            product_promotion = 5,
                            category_id = 2
                        },new Product()
                        {
                            product_name = "áo sơ mi dài tay",
                            product_image = "https://product.hstatic.net/200000584505/product/_mg_0115-edit_1a3d197ba9d4434aa706a2fdb1a99f88_large.jpg",
                            product_description = "áo sơ mi dài tay",
                            product_price = 485000,
                            product_promotion = 9,
                            category_id = 3
                        },new Product()
                        {
                            product_name = "áo sơ mi gile",
                            product_image = "https://product.hstatic.net/200000584505/product/_mg_0009-edit_d18821c851df4e648e0352f9a3670c7f_large.jpg",
                            product_description = "áo sơ mi gile",
                            product_price = 459000,
                            product_promotion = 6,
                            category_id = 3
                        },new Product()
                        {
                            product_name = "áo sơ mi tay lở",
                            product_image = "https://product.hstatic.net/200000584505/product/img_7529-edit_6d4a41824f1d4757b1ddbf800ed87c9e_large.jpg",
                            product_description = "áo sơ mi tay lở",
                            product_price = 439000,
                            product_promotion = 7,
                            category_id = 3
                        }
                    });
                context.SaveChanges();
            }
            //Users
            if (!context.Users.Any())
            {
                context.Users.AddRange(new List<User>()
                    {
                        new User()
                        {
                            user_name = "ngtuan9870",
                            user_password = "12345678",
                            user_email = "ngtuan9870@gmail.com",
                            user_phone = "0339623653",
                            user_level = 3
                        },new User()
                        {
                            user_name = "ducthuangtvt",
                            user_password = "12345678",
                            user_email = "ducthuangtvt@gmail.com",
                            user_phone = "0333531615",
                            user_level = 3
                        },new User()
                        {
                            user_name = "thanhnguyen792",
                            user_password = "12345678",
                            user_email = "thanhnguyen792@gmail.com",
                            user_phone = "0339545121",
                            user_level = 3
                        }
                     });
                context.SaveChanges();
            }
        }
    }
}
