using BNS.DataAccess.Data.Repository.IRepository;
using BNS.Models;
using BNS.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BNS.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public ProjectVM ProjectVM { get; set; }
        public ProjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(FillProjectData());
        }

        public IActionResult ViewTree()
        {
            return View(FillProjectData());
        }

        public List<Project> FillProjectData()
        {
            var objectDB = _unitOfWork.Project.GetAll(i => i.IsRemoved == false, null , includeProperties: "ProjectType");

            return objectDB.ToList();
        }

       
        public IActionResult Upsert(long? id)
        {
            ProjectVM = new ProjectVM()
            {
                Project = new Project(),
                ProjectTypeList = _unitOfWork.ProjectType.ProjectTypeListForDropDown().ToList(),
                ProjectList = _unitOfWork.Project.ProjectListForDropDown().ToList()
            };
            if (id != null)
            {
                ProjectVM.Project = _unitOfWork.Project.Get(id.GetValueOrDefault());
            }
            return View(ProjectVM);

        }

        [HttpPost]
        public IActionResult Upsert(Project project)
        {
            if (ModelState.IsValid)
            {

                if (project.ID == 0)
                {
                    //New Project

                    _unitOfWork.Project.Add(project);
                }
                else
                {

                    //Edit Project

                    _unitOfWork.Project.Update(project);
                }
                _unitOfWork.Save();

            }
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(long? id)
        {
            Project project = new Project();

            if (id == null)
            {
                return View(project);
            }
            project = _unitOfWork.Project.Get(id.GetValueOrDefault());

            _unitOfWork.Project.Remove(project);

            return RedirectToAction(nameof(Index));
        }
    }
}
