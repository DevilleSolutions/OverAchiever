using System.ComponentModel.DataAnnotations;

namespace OverAchiever.Web.Models
{
    public interface ILoginModel
    {
        [Required]
        [Display(Name = "User name")]
        string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        string Password { get; set; }

        [Display(Name = "Remember me?")]
        bool RememberMe { get; set; }
    }
    /*
    public class LoginModel : ILoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }*/
}