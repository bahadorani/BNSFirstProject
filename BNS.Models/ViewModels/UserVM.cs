using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BNS.Models.ViewModels
{
    public class UserVM
    {
        public User User { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
        public IEnumerable<SelectListItem> SupervisorList { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
        public IEnumerable<SelectListItem> ContractTypeList { get; set; }

       public List<ContractType> ContractTypes = new List<ContractType>()
            {
                new ContractType(){ ID = 1, Caption="رسمی"},
                new ContractType(){ ID = 2, Caption="قراردادی"},
                new ContractType(){ ID = 3, Caption="آزمایشی"}
            };
        public List<UserStatus> UserStatusList = new List<UserStatus>()
            {
                new UserStatus(){ ID = 1, Caption="فعال"},
                new UserStatus(){ ID = 2, Caption="غیرفعال"}
            };

        
    }
}
