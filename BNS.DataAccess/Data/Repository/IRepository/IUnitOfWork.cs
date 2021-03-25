using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.DataAccess.Data.Repository.IRepository
{
   public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        IProjectTypeRepository ProjectType { get; }
        IProjectRepository Project { get; }
        void Save();
    }
}
