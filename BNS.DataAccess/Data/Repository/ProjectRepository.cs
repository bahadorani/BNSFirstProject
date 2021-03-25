using BNS.DataAccess.Data.Repository.IRepository;
using BNS.Models;
using BNS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BNS.DataAccess.Data.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly AppDbContext _db;
       // private ProjectVM projectVM;
        public ProjectRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Remove(Project project)
        {
            var objFromDb = _db.Project.FirstOrDefault(s => s.ID == project.ID);
            objFromDb.IsRemoved = true;
            objFromDb.RemoveTime = DateTime.Today;

            _db.SaveChanges();
        }

        public void Update(Project project)
        {
            var objFromDb = _db.Project.FirstOrDefault(s => s.ID == project.ID);
            objFromDb.ProjectName = project.ProjectName;
            objFromDb.ProjectTypeID = project.ProjectTypeID;

            objFromDb.UpdateTime = DateTime.Today;
            _db.SaveChanges();
        }

        public IEnumerable<SelectListItem> ProjectListForDropDown()
        {

            return _db.Project.Where(i => i.IsRemoved == false).Select(i => new SelectListItem()
            {
                Text = i.ProjectName,
                Value = i.ID.ToString()
            });
        }
        //public IEnumerable<SelectListItem> ParentProjectList()
        //{
        //    return _db.Project.Where(i => i.ParentID == null).Select(i => new SelectListItem()
        //    {
        //        Text = i.ProjectName,
        //        Value = i.ID.ToString()
        //    });
        //}
        //public IEnumerable<SelectListItem> ChildProjectList(long Id)
        //{
        //    return _db.Project.Where(i => i.ParentID == Id).Select(i => new SelectListItem()
        //    {
        //        Text = i.ProjectName,
        //        Value = i.ID.ToString()
        //    });
        //}
    }
}
