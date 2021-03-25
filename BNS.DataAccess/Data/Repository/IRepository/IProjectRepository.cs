using BNS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.DataAccess.Data.Repository.IRepository
{
   public interface IProjectRepository : IRepository<Project>
    {
        IEnumerable<SelectListItem> ProjectListForDropDown();

        void Update(Project project);
        void Remove(Project project);
    }
}
