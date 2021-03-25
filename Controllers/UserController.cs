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
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public UserVM UserVM { get; set; }
        public UserController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            var objectDB = _unitOfWork.User.GetAll(i => i.IsRemoved == false,null ,includeProperties: "Role,Users");

            return View(objectDB.ToList());
        }
        public IActionResult Upsert(long? id)
        {
            UserVM = new UserVM()
            {
                User = new User(),
                RoleList = _unitOfWork.Role.GetRoleListForDropDown().ToList(),
                SupervisorList = _unitOfWork.User.GetUserListForDropDown().ToList(),
                ContractTypeList = _unitOfWork.User.ContractTypeListForDropDown(),
                StatusList = _unitOfWork.User.UserStatusListForDropDown(),
            };
            if (id != null)
            {
                UserVM.User = _unitOfWork.User.Get(id.GetValueOrDefault());
            }
            //Model.RoleList as IEnumerable<SelectListItem>
            return View(UserVM);

        }

        [HttpPost]
        public IActionResult Upsert(User user)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (UserVM.User.ID == 0)
                {
                    //New User
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\users");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    UserVM.User.ImageUrl = @"\images\users\" + fileName + extension;

                    _unitOfWork.User.Add(UserVM.User);
                }
                else
                {
                    
                    //Edit User
                    var serviceFromDb = _unitOfWork.User.Get(UserVM.User.ID);
                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"images\users");
                        var extension_new = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, serviceFromDb.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        UserVM.User.ImageUrl = @"\images\users\" + fileName + extension_new;
                    }
                    else
                    {
                        UserVM.User.ImageUrl = serviceFromDb.ImageUrl;
                    }

                    _unitOfWork.User.Update(UserVM.User);
                }
                _unitOfWork.Save();
                
            }
            //else
            //{
            //    // UserVM.SupervisorList = _unitOfWork.User.GetUserListForDropDown();
            //    // UserVM.RoleList = _unitOfWork.Role.GetRoleListForDropDown();
            //    return RedirectToAction(nameof(Index));
            //}
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(long? id)
        {
            User user = new User();

            if (id == null)
            {
                return View(user);
            }
            user = _unitOfWork.User.Get(id.GetValueOrDefault());

            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, user.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }


            _unitOfWork.User.Remove(user);


            return RedirectToAction(nameof(Index));
        }


    }
}
