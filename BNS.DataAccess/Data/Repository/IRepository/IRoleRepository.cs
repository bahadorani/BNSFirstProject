using BNS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.DataAccess.Data.Repository.IRepository
{
   public interface IRoleRepository :IRepository<Role>
    {
        IEnumerable<SelectListItem> GetRoleListForDropDown();

        void Update(Role role);
        void Remove(Role role);
    }
}
