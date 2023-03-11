using System.ComponentModel.DataAnnotations;

namespace EasyOverTime.Models.Login
{
    public class RegisterModel
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

    }
}
