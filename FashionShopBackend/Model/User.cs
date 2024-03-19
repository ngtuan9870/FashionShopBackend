using System.ComponentModel.DataAnnotations;

namespace FashionShopBackend.Model
{
    public class User
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_password { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string user_email { get; set; }
        public string user_phone { get; set; }
        public DateTime user_birthday { get; set; }
        public int user_level { get; set; }
    }
}
