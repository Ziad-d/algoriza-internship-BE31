using Domain.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public IBaseRepository<Specialization> Specializations { get; private set; }
        public IBaseRepository<Appointment> Appointments { get; private set; }
        public IBaseRepository<Request> Requests { get; private set; }
        public IBaseRepository<Booking> Bookings { get; private set; }
        public IUserRepository AuthRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            Specializations = new BaseRepository<Specialization>(context);
            Appointments = new BaseRepository<Appointment>(context);
            Requests = new BaseRepository<Request>(context);
            Bookings = new BaseRepository<Booking>(context);
            AuthRepository = new UserRepository(userManager);
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
