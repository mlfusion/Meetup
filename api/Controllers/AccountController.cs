using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Entities;
using api.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers {
    public class AccountController : BaseApiController {
        private readonly ITokenService _tokenService;
        public AccountController (DataContext context, ITokenService tokenService) : base (context) {
            _tokenService = tokenService;
        }

        [HttpPost ("register")]
        public async Task<ActionResult<UserDto>> Register (RegisterDto registerDto) {

            if (await UserExist (registerDto.UserName)) return BadRequest ("Username is already taken");

            using var hmac = new HMACSHA512 ();

            var user = new AppUser {
                UserName = registerDto.UserName,
                PasswordHash = hmac.ComputeHash (Encoding.UTF8.GetBytes (registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            await _context.Users.AddAsync (user);
            await _context.SaveChangesAsync ();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost ("login")]
        public async Task<ActionResult<UserDto>> Login (LoginDto loginDto) {
            var user = await _context.Users
                .SingleOrDefaultAsync (x => x.UserName.ToLower () == loginDto.UserName.ToLower ());

            if (user == null)
                return Unauthorized ("Invalid username");

            using var hmac = new HMACSHA512 (user.PasswordSalt);

            var computerHash = hmac.ComputeHash (Encoding.UTF8.GetBytes (loginDto.Password));

            for (int i = 0; i < computerHash.Length; i++) {
                if (computerHash[i] != user.PasswordHash[i]) return Unauthorized ("Invalid passwoord");
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExist (string userName) {
            return await _context.Users.AnyAsync (x => x.UserName.ToLower () == userName.ToLower ());
        }
    }
}