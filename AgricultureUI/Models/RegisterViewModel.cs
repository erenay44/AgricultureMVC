using System.ComponentModel.DataAnnotations;

namespace AgricultureUI.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Lütfen kullanıcı adı giriniz!")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Lütfen mail giriniz!")]
        public string mail { get; set; }

        [Required(ErrorMessage = "Lütfen şifre giriniz!")]
        public string password { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi tekrar giriniz giriniz!")]
        [Compare("password",ErrorMessage ="Şifreler uyumlu değil!")]
        public string passwordConfirm { get; set; }
    }
}
