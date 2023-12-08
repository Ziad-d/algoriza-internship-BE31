using Domain.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public IUserRepository AuthRepository { get; private set; }
        public IBaseRepository<Appointment> Appointments { get; private set; }
        public IBaseRepository<Booking> Bookings { get; private set; }
        public IBaseRepository<Specialization> Specializations { get; private set; }
        public IBaseRepository<Request> Requests { get; private set; }
        public IBaseRepository<DayTime> Time { get; private set; }

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            AuthRepository = new UserRepository(userManager);
            Appointments = new BaseRepository<Appointment>(context);
            Bookings = new BaseRepository<Booking>(context);
            Specializations = new BaseRepository<Specialization>(context);
            Requests = new BaseRepository<Request>(context);
            Time = new BaseRepository<DayTime>(context);
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
