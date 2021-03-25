using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BNS.Models
{
  public  class User :BaseEntity
    {

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "لطفا رمز عبور را وارد نمایید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

  
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Mobile { get; set; }


        [MaxLength(100)]
        public string FullName { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public int ContractType { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public string EmploymentDate { get; set; }


        [Required]
        public long RoleID { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }


        public long? SupervisorID { get; set; }

        [ForeignKey("SupervisorID")]
        public virtual User Supervisor { get; set; }


        public virtual IEnumerable<User> Users { get; set; }
       // public virtual IList<UserContract> UserContracts { get; set; }

    }
}
