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
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _db;
        private UserVM uservm;
        public UserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetUserListForDropDown()
        {
            return _db.User.Where(i=>i.IsRemoved==false).Select(i => new SelectListItem()
            {
                Text = i.FullName,
                Value = i.ID.ToString()
            });
        }
        public IEnumerable<SelectListItem> ContractTypeListForDropDown()
        {
            uservm = new UserVM();
            return uservm.ContractTypes.Select(i => new SelectListItem()
            {
                Text = i.Caption,
                Value = i.ID.ToString()
            });
        }
        public IEnumerable<SelectListItem> UserStatusListForDropDown()
        {
            uservm = new UserVM();
            return uservm.UserStatusList.Select(i => new SelectListItem()
            {
                Text = i.Caption,
                Value = i.ID.ToString()
            });
        }

        public void Update(User user)
        {
            var objFromDb = _db.User.FirstOrDefault(s => s.ID == user.ID);
            objFromDb.UserName = user.UserName;
            objFromDb.Password = user.Password;
            objFromDb.Email = user.Email;
            objFromDb.Mobile = user.Mobile;
            objFromDb.FullName = user.FullName;
            objFromDb.ImageUrl = user.ImageUrl;
            objFromDb.Status = user.Status;
            objFromDb.ContractType = user.ContractType;
            objFromDb.RoleID = user.RoleID;
            objFromDb.SupervisorID = user.SupervisorID;

            objFromDb.UpdateTime = DateTime.Today;
            _db.SaveChanges();
        }

        public void Remove(User user)
        {
            var objFromDb = _db.User.FirstOrDefault(s => s.ID == user.ID);
             objFromDb.IsRemoved = true;
            objFromDb.RemoveTime = DateTime.Today;

            _db.SaveChanges();
        }
    }
}
