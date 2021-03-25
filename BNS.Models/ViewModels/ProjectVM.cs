using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Models.ViewModels
{
   public class ProjectVM
    {
        public Project Project { get; set; }

        public IEnumerable<SelectListItem> ProjectList { get; set; }
        public IEnumerable<SelectListItem> ProjectTypeList { get; set; }
    }
}
