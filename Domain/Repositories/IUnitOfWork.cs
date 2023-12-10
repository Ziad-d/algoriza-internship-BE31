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
        IUserRepository AuthRepository { get; }
        IBaseRepository<Appointment> Appointments { get; }
        IBaseRepository<Booking> Bookings { get; }
        IBaseRepository<Specialization> Specializations { get; }
        IBaseRepository<Request> Requests { get; }
        IBaseRepository<DayTime> Time { get; }
        IBaseRepository<DiscountCode> DiscountCodes { get; }
        IBaseRepository<ExpiredCode> ExpiredCodes { get; }
        
        int Complete();
    }
}
