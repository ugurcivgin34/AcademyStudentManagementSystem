using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.Models
{
    [Table("Cities")]
    public class City : Base<byte>
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "İl adı en fazla 50 en az 2 karakter aralığında olmalıdır!")]
        public string CityName { get; set; }

        [Required]
        //TODO isUnique
        [StringLength(50, MinimumLength = 2, ErrorMessage = "İl adı en fazla 50 en az 2 karakter aralığında olmalıdır!")]
        public string PlateCode { get; set; }

        //İlişkiler kurulacak

        public virtual ICollection<District> Districts { get; set; }
        //Yukarıdakini kullanırsak,ToList() yapmak gerekiyor.Eriniyorsak .ToList() yapmaya aşağıdakini kullanırız
        //public virtual List<District> Districts { get; set; }
    }
}
