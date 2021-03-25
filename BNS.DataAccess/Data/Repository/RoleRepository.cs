using BNS.DataAccess.Data.Repository.IRepository;
using BNS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BNS.DataAccess.Data.Repository
{
   public class RoleRepository :Repository<Role>, IRoleRepository
    {
        private readonly AppDbContext _db;
        public RoleRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetRoleListForDropDown()
        {
            return _db.Role.Where(i=>i.IsRemoved == false).Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.ID.ToString()
            });
        }

        public void Update(Role role)
        {
             var objFromDb = _db.Role.FirstOrDefault(s => s.ID == role.ID);
             objFromDb.Name = role.Name;
             objFromDb.UpdateTime = DateTime.Today;
            _db.SaveChanges();
        }

        public void Remove(Role role)
        {
            var objFromDb = _db.Role.FirstOrDefault(s => s.ID == role.ID);
            objFromDb.IsRemoved = true;
            objFromDb.RemoveTime = DateTime.Today;
            _db.SaveChanges();
        }
    }
}
