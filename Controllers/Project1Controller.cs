using BNS.DataAccess.Data.Repository.IRepository;
using BNS.Models;
using BNS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.Controllers
{
    public class Project1Controller : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public Project1Controller(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
           
            
            return View();
        }

        private static List<TreeNodeVM> FillRecursive(List<Project> flatObjects, long? parentId = null)
        {
            return flatObjects.Where(x => x.ParentID.Equals(parentId)).Select(item => new TreeNodeVM
            {
                ProjectName = item.ProjectName,
                text = item.ProjectName,
                ProjectId = item.ID,
                Children = FillRecursive(flatObjects, item.ID),
                nodes = FillRecursive(flatObjects, item.ID)
            }).ToList();
        }

        public JsonResult Get(string query)
        {
            var objectDB = _unitOfWork.Project.GetAll(i => i.IsRemoved == false, null, null);
            List<TreeNodeVM> Tree = FillRecursive(objectDB.ToList(), null);
            return this.Json(Tree);
        }
    }
}
