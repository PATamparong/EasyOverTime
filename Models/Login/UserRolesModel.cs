using System.ComponentModel.DataAnnotations;

namespace EasyOverTime.Models.Login
{
    public class UserRolesModel
    {
        [Required]
        public string? WithAuthority { get; set; }
        [Required]
        public string? NormalEmployee { get; set; }

        public bool ShowWithAuthority => !string.IsNullOrEmpty(WithAuthority);
        public bool ShowNormalEmployee => !string.IsNullOrEmpty(NormalEmployee);
    }
}
