using FashionShopBackend.Dto;
using FashionShopBackend.Interface;
using FashionShopBackend.Model;
using FashionShopNETCoreAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FashionShopBackend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FashionShopDBContext _context;
        private readonly AppSettings _appSettings;
        public UserRepository(FashionShopDBContext context, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }
        public void addUser(UserDto user)
        {
            var u = new User
            {
                user_name = user.user_name,
                user_email = user.user_email,
                user_level = user.user_level,
                user_birthday = user.user_birthday,
                user_password = user.user_password,
                user_phone = user.user_phone,
            };
            _context.Add(u);
            _context.SaveChanges();
        }

        public void deleteUser(int id)
        {
            var user_delete = _context.Users.SingleOrDefault(u => u.user_id == id);
            _context.Users.Remove(user_delete);
            _context.SaveChanges();
        }

        public void editUser(UserDto user)
        {
            var user_edit = _context.Users.SingleOrDefault(u => u.user_id == user.user_id);
            user_edit.user_name = user.user_name;
            user_edit.user_email = user.user_email;
            user_edit.user_level = user.user_level;
            user_edit.user_birthday = user.user_birthday;
            user_edit.user_password = user.user_password;
            user_edit.user_phone = user.user_phone;
            _context.SaveChanges();
        }

        public ICollection<User> getAllUser()
        {
            return _context.Users.OrderBy(u => u.user_id).ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Where(u => u.user_id == id).FirstOrDefault();
        }


        public string getToken(UserDto user)
        {
            var user_login = _context.Users.SingleOrDefault(u => u.user_email == user.user_email && u.user_password == user.user_password);
            if (user_login == null)
            {
                return null;
            }
            return GenerateJwtToken(user);
        }
        public bool login(UserDto user)
        {
            return true;
        }

        public string GenerateJwtToken(UserDto user)
        {
            var jwtTokenHanlder = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.user_name),
                    new Claim(ClaimTypes.Email, user.user_email),
                    new Claim("user_id", user.user_id.ToString()),

                    //roles
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHanlder.CreateToken(tokenDescription);
            return jwtTokenHanlder.WriteToken(token);
        }

        public void register(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
