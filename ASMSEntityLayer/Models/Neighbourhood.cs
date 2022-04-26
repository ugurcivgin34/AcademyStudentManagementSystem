using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.Models
{
    [Table("Neighbourhoods")]
    public class Neighbourhood :Base<int>
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Mahalle adı en fazla 50 en az 2 karakter aralığında olmalıdır!")]
        public string NeighbourhoodName { get; set; }

        //ilişki ilçeyle

        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }

        //ilişki
        public virtual ICollection<UsersAddress> UsersAddresses { get; set; }

    }
}
