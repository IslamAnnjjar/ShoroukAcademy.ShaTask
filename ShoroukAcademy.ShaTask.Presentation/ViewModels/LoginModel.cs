using System.ComponentModel.DataAnnotations;

namespace ShoroukAcademy.ShaTask.Presentation.ViewModels
{
    public class LoginModel
    {
        [Required, MinLength(3)]
        public string UserName { get; set; }
        [Required, MinLength(3), DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
