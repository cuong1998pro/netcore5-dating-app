using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        //[StringLength(6)]
        public string Password { get; set; }
    }
} 