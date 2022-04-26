﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.Models
{
    public class Base<T> : IBase
    {
        [Key]
        [Column(Order =1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
        public int MyProperty { get; set; }

        [Column(Order = 2)]
        [DataType(DataType.DateTime)]
        [Display(Name ="Oluşturulma Tarihi")]
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
