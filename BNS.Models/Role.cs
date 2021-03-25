using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BNS.Models
{
   public class Role:BaseEntity
    {
        [Required(ErrorMessage = "لطفا عنوان را وارد نمایید")]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
