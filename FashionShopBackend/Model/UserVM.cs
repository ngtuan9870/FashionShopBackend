namespace FashionShopNETCoreAPI.Model
{
    public class UserVM
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_password { get; set; }
        public string user_email { get; set; }
        public string user_phone { get; set; }
        public DateTime user_birthday { get; set; }
        public int user_level { get; set; }
    }
}
