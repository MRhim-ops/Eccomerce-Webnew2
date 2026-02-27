using System.ComponentModel.DataAnnotations;

namespace Eccomerce_Web.Dtos
{
    public class RegisterDto
    {

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}