using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BNS.Models
{
   public class ProjectType:BaseEntity
    {
        [Required]
        public string Caption { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
