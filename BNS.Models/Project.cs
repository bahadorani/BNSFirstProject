using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BNS.Models
{
  public  class Project:BaseEntity
    {
        [Required]
        public string ProjectName { get; set; }

        [Required]
        public long ProjectTypeID { get; set; }

        [ForeignKey("ProjectTypeID")]
        public virtual ProjectType ProjectType { get; set; }

        public long? ParentID { get; set; }
        [ForeignKey("ParentID")]
        public Project Parent { get; set; }
        public ICollection<Project> SubProjects { get; set; } 


        //-------------------------------------------------------------------------------

        //public int Level { get; set; }= new List<Project>();
        //public string Path { get; set; }
    }
}
