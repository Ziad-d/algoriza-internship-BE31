using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Specialization> Specializations { get; }
        IUserRepository AuthRepository { get; }

        int Complete();
    }
}
