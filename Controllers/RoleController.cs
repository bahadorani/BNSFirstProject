using BNS.DataAccess.Data.Repository.IRepository;
using BNS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.Controllers
{

    public class RoleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var objectDB = _unitOfWork.Role.GetAll(i => i.IsRemoved == false,null,null);

            return View(objectDB.ToList());
        }
        public IActionResult Upsert(long? id)
        {
            Role role = new Role();
            if (id == null)
            {
                return View(role);
            }
            role = _unitOfWork.Role.Get(id.GetValueOrDefault());
            if (role == null)
            {
                return NotFound();
            }
            return View(role);

        }

        [HttpPost]
        public IActionResult Upsert(Role role)
        {
            if (ModelState.IsValid)
            {
                if (role.ID == 0)
                {
                    _unitOfWork.Role.Add(role);
                }
                else
                {
                    _unitOfWork.Role.Update(role);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }
        public IActionResult Delete(long? id)
        {
            Role role = new Role();
            if (id == null)
            {
                return View(role);
            }
            role = _unitOfWork.Role.Get(id.GetValueOrDefault());
           _unitOfWork.Role.Remove(role);
            return RedirectToAction(nameof(Index));
        }

       
    }
}
