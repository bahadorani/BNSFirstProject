using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Models.ViewModels
{
   public class TreeNodeVM
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string text { get; set; }

        public TreeNodeVM ParentProject { get; set; }
        public List<TreeNodeVM> Children { get; set; }
        public List<TreeNodeVM> nodes { get; set; }
    }
}
