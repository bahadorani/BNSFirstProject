using BNS.DataAccess.Data.Repository.IRepository;
using BNS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.Controllers
{
    public class ProjectTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var objectDB = _unitOfWork.ProjectType.GetAll(i => i.IsRemoved == false, null, null);
            return View(objectDB.ToList());
        }

        public IActionResult Upsert(long? id)
        {
            ProjectType projectType = new ProjectType();
            if (id == null)
            {
                return View(projectType);
            }
            projectType = _unitOfWork.ProjectType.Get(id.GetValueOrDefault());
            if (projectType == null)
            {
                return NotFound();
            }
            return View(projectType);

        }

        [HttpPost]
        public IActionResult Upsert(ProjectType projectType)
        {
            if (ModelState.IsValid)
            {
                if (projectType.ID == 0)
                {
                    _unitOfWork.ProjectType.Add(projectType);
                }
                else
                {
                    _unitOfWork.ProjectType.Update(projectType);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(projectType);
        }
        public IActionResult Delete(long? id)
        {
            ProjectType projectType = new ProjectType();
            if (id == null)
            {
                return View(projectType);
            }
            projectType = _unitOfWork.ProjectType.Get(id.GetValueOrDefault());
            _unitOfWork.ProjectType.Remove(projectType);
            return RedirectToAction(nameof(Index));
        }
    }
}
