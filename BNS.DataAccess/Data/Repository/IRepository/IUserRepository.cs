using BNS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.DataAccess.Data.Repository.IRepository
{
   public interface IUserRepository : IRepository<User>
    {
        IEnumerable<SelectListItem> GetUserListForDropDown();
        IEnumerable<SelectListItem> ContractTypeListForDropDown();
        IEnumerable<SelectListItem> UserStatusListForDropDown();

        void Update(User user);
        void Remove(User user);
    }
}
