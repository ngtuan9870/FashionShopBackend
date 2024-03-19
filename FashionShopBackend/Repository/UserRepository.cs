using FashionShopBackend.Dto;
using FashionShopBackend.Interface;
using FashionShopBackend.Model;
using FashionShopNETCoreAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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


        public async Task<TokenModel> getToken(LoginModel user)
        {
            var user_login = _context.Users.SingleOrDefault(u => u.user_email == user.user_email && u.user_password == user.user_password);
            if (user_login == null)
            {
                return null;
            }
            var token = await GenerateJwtToken(user_login);
            return token;
        }
        public bool login(UserDto user)
        {
            return true;
        }

        public async Task<TokenModel> GenerateJwtToken(User user)
        {
            var jwtTokenHanlder = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.user_name),
                    new Claim(JwtRegisteredClaimNames.Email, user.user_email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.user_email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("user_id", user.user_id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHanlder.CreateToken(tokenDescription);
            var accessTK = jwtTokenHanlder.WriteToken(token);
            var refreshTK = GenerateFreshJwtToken();
            //Save to Database
            var refreshTokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                JwtId = token.Id,
                user_id = user.user_id,
                Token = refreshTK,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(1)
            };
            await _context.AddAsync(refreshTokenEntity);
            await  _context.SaveChangesAsync();
            return new TokenModel
            {
                AccessToken = accessTK,
                RefreshToken = refreshTK
            };
        }

        public string GenerateFreshJwtToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
            }
                return Convert.ToBase64String(random);
        }

        public async Task<ApiResponse> ReNewToken(TokenModel tokenModel)
        {
            var jwtTokenHanlder = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenValidateParam = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false
            };
            try
            {
                var tokenIsVerification = jwtTokenHanlder.ValidateToken(tokenModel.AccessToken, tokenValidateParam, out var validatedToken);
                if(validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        return new ApiResponse { success = false,message = "Token không đúng format" };
                    }
                }
                var utcExpiredDate = long.Parse(tokenIsVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expiredDate = ConvertUnixTimeToDateTime(utcExpiredDate);
                if(expiredDate > DateTime.UtcNow)
                {
                    return new ApiResponse { success = false, message = "Token hết hạn rồi!" };
                }
                var storedToken = _context.refreshTokens.FirstOrDefault(x => x.Token == tokenModel.RefreshToken);
                if (storedToken == null)
                {
                    return new ApiResponse { success = false, message = "Không thấy Token" };
                }
                if (storedToken.IsUsed) return new ApiResponse { success = false, message = "Token đã được sử dụng!" };
                if (storedToken.IsRevoked) return new ApiResponse { success = false, message = "Token đã bị thu hồi!" };
                var jti = tokenIsVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    return new ApiResponse { success = false, message = "Token trùng id" };
                }
                storedToken.IsRevoked = true;
                storedToken.IsUsed = true;
                _context.Update(storedToken);
                await _context.SaveChangesAsync();
                var user = await _context.Users.SingleOrDefaultAsync(u => u.user_id == storedToken.user_id);
                var token = await GenerateJwtToken(user);
                return new ApiResponse { success = true, message = "Ok! Đã tạo mới!" }; ;
            }
            catch(Exception ex) 
            {
                return new ApiResponse { success = false, message = "Lỗi hệ thống!" };
            }
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpiredDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpiredDate).ToUniversalTime();
            return dateTimeInterval;
        }

        public void register(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
