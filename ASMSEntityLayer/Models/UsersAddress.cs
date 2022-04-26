using ASMSEntityLayer.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.Models
{
    [Table("UsersAddresses")]
    public class UsersAddress : Base<int>
    {
        public string UserId { get; set; } //AspnetUsers ilişki

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Adres adı en fazla 50 en az 2 karakter aralığında olmalıdır!")]
        public string AdressTitle { get; set; }
        public int NeighbourhoodId { get; set; } //Mahalleyle ilişki

        [Required]
        [StringLength(500, ErrorMessage = "Adres detayı  en fazla 500  karakter aralığında olmalıdır!")]
        public string AdressDetails { get; set; }

        [StringLength(5,MinimumLength =5, ErrorMessage = "Posta Kodu 5 karakter olmalıdır!")]
        public string PostCode { get; set; } // 34000

        //ilişkiler buradakilerin virtual list versionlarını oluşturmayalım

        [ForeignKey("UserId")]
        public virtual AppUser AspUser { get; set; }

        [ForeignKey("NeighbourhoodId")]
        public virtual Neighbourhood Neighbourhood { get; set; }

    }
}
