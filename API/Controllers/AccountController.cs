using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using API.Data;
using API.Entity;
using Microsoft.AspNetCore.Mvc;
using API.DTO;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            using var hmac = new HMACSHA512();
            byte[] passwordByte = Encoding.UTF8.GetBytes(registerDTO.Password);
            if (await UserExist(registerDTO.Username))
            {
                //400
                return BadRequest("Username is taken!");
            }
            var user = new AppUser()
            {
                Username = registerDTO.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(passwordByte),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDTO
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login)
        {
            var user = await _context.Users
            .SingleOrDefaultAsync(x => x.Username.Equals(login.Username.ToLower()));
            if (user == null) //401
                return Unauthorized("Invalid username");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var passwordByte = Encoding.UTF8.GetBytes(login.Password);
            var computedHash = hmac.ComputeHash(passwordByte);
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid password");
            }
            return new UserDTO
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username.Equals(username.ToLower()));
        }

    }
}