using FashionShopBackend.Dto;
using FashionShopBackend.Interface;
using FashionShopBackend.Model;
using FashionShopBackend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionShopBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetAllUser()
        {
            var users = _userRepository.getAllUser();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        [HttpGet("{user_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult getUserByID(int user_id)
        {
            var user = _userRepository.GetUserById(user_id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }
        [HttpPost]
        [Authorize]
        public IActionResult addUser(UserDto user)
        {
            _userRepository.addUser(user);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }
        [HttpPut]
        [Authorize]
        public IActionResult editUser(UserDto user)
        {
            _userRepository.editUser(user);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }
        [HttpDelete]
        [Authorize]
        public IActionResult deleteUser(int id)
        {
            _userRepository.deleteUser(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok();
        }
        [HttpPost("login")]
        public IActionResult login(LoginModel user)
        {
            return Ok(new
            {
                Success = true,
                Message = _userRepository.getToken(user)
            });
        }
        [HttpPost("reNewToken")]
        public async Task<IActionResult> ReNewToken(TokenModel tokenModel)
        {
            var check = await _userRepository.ReNewToken(tokenModel);
            if (check.success)
            {
                return Ok(check);
            }
            return BadRequest();
        }
    }
}
