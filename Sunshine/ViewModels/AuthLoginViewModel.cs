using System.ComponentModel.DataAnnotations;

namespace Sunshine.ViewModels
{
    public class AuthLoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email не може бути порожнім")]
        [EmailAddress(ErrorMessage = "Email є не коректим")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль не може бути порожнім")]
        [MinLength(3, ErrorMessage = "Пароль не може коротшим ніж 3 символи")]
        public string Password { get; set; }
    }
}
