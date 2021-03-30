using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }

    public class LoginDto 
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }   
    }

    public class UserDto
    {
         [Required]
        public string Username { get; set; }
         [Required]
        public string Token { get; set; }
    }
}