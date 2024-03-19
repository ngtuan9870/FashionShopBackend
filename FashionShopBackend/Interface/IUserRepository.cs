using FashionShopBackend.Dto;
using FashionShopBackend.Model;

namespace FashionShopBackend.Interface
{
    public interface IUserRepository
    {
        ICollection<User> getAllUser();
        User GetUserById(int id);
        void addUser(UserDto user);
        void editUser(UserDto user);
        void deleteUser(int id);
        bool login(UserDto user);
        void register(UserDto user);
        Task<TokenModel> GenerateJwtToken(User user);
        Task<TokenModel> getToken(LoginModel user);
        string GenerateFreshJwtToken();
        Task<ApiResponse> ReNewToken(TokenModel tokenModel);
    }
}
