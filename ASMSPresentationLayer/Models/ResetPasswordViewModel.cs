using System.ComponentModel.DataAnnotations;

namespace ASMSPresentationLayer.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required(ErrorMessage = "Yeni Şifre alanı zorunludur!")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Şifreniz minimum 8 maksimum 20 haneli olmalıdır!")]
        [Display(Name = "Yeni Şifre")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Yeni Şifre Tekrar alanı zorunludur!")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre Tekrarı")]
        [Compare(nameof(NewPassword), ErrorMessage = "Şifreler Uyuşmuyor!")]
        public string ConfirmNewPassword { get; set; }

    }
}
