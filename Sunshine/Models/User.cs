using System.ComponentModel.DataAnnotations;

namespace Sunshine.Models
{
    public class User : BaseModel
    {
        [Required(ErrorMessage = "Введіть текст")]
        public string Email { get; set; }
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }
        public string Password { get; set; }
        [Display(Name = "Підтверджений email")]
        public bool IsConfirmedEmail { get; set; }
        public string Role { get; set; }
    }
}
