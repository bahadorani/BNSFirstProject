using BNS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.DataAccess.Data.Repository.IRepository
{
  public interface IProjectTypeRepository : IRepository<ProjectType>
    {
        IEnumerable<SelectListItem> ProjectTypeListForDropDown();

        void Update(ProjectType projectType);
        void Remove(ProjectType projectType);
    }
}
