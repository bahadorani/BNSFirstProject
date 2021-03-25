using BNS.DataAccess.Data.Repository.IRepository;
using BNS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BNS.DataAccess.Data.Repository
{
   public class ProjectTypeRepository : Repository<ProjectType>, IProjectTypeRepository
    {
        private readonly AppDbContext _db;
        public ProjectTypeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Remove(ProjectType projectType)
        {
            var objFromDb = _db.ProjectType.FirstOrDefault(s => s.ID == projectType.ID);
            objFromDb.IsRemoved = true;
            objFromDb.RemoveTime = DateTime.Today;

            _db.SaveChanges();
        }

        public void Update(ProjectType projectType)
        {
            var objFromDb = _db.ProjectType.FirstOrDefault(s => s.ID == projectType.ID);
            objFromDb.Caption = projectType.Caption;

            objFromDb.UpdateTime = DateTime.Today;
            _db.SaveChanges();
        }

        public IEnumerable<SelectListItem> ProjectTypeListForDropDown()
        {
            return _db.ProjectType.Where(i => i.IsRemoved == false).Select(i => new SelectListItem()
            {
                Text = i.Caption,
                Value = i.ID.ToString()
            });
        }
    }

}
