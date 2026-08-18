using System.ComponentModel.DataAnnotations;

namespace AgricultureUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="Lütfen kullanıcı adını giriniz")]
        public string username { get; set; }
        [Required(ErrorMessage = "Lütfen şifre giriniz")]
        public string password { get; set; }
    }
}
