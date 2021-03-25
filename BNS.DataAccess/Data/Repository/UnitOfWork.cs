using BNS.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

       public UnitOfWork(AppDbContext context )
        {
            _context = context;
            User = new UserRepository(_context);
            Role = new RoleRepository(_context);
            ProjectType = new ProjectTypeRepository(_context);
            Project = new ProjectRepository(_context);
        }


        public IUserRepository User { get;private set; }
        public IRoleRepository Role { get;private set; }

        public IProjectTypeRepository ProjectType { get; private set; }

        public IProjectRepository Project{ get; private set; }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
