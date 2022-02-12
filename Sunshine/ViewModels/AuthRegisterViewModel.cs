using System.ComponentModel.DataAnnotations;

namespace Sunshine.ViewModels
{
    public class AuthRegisterViewModel: AuthLoginViewModel
    {
        [Display(Name = "Ім'я")]
        [Required(ErrorMessage = "Ім'я не може бути порожнім")]
        public string FirstName { get; set; }

        [Display(Name = "Прізвище")]
        [Required(ErrorMessage = "Прізвище не може бути порожнім")]
        public string LastName { get; set; }
    }
}
