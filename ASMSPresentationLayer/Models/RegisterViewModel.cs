﻿using ASMSEntityLayer.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ASMSPresentationLayer.Models
{
    public class RegisterViewModel
    {

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TCKimlik numarası 11 haneli olmalıdır!")]
        public string TCNumber { get; set; }

        [Required(ErrorMessage = "İsim Gereklidir!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "İsminiz en az 2 en çok 50 karakter olmalıdır!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyisim Gereklidir!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyisminiz en az 2 en çok 50 karakter olmalıdır!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email zorunludur!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre alanı zorunludur!")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Şifreniz minimum 8 maksimum 20 haneli olmalıdır!")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [Required(ErrorMessage = "Cinsiyet seçimi Gereklidir!")]
        public Genders Gender { get; set; }


    }
}
