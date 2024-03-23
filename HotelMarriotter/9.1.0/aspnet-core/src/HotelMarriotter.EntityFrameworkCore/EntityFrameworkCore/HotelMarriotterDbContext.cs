using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using HotelMarriotter.Authorization.Roles;
using HotelMarriotter.Authorization.Users;
using HotelMarriotter.MultiTenancy;
using HotelMarriotter.DbEntities;

namespace HotelMarriotter.EntityFrameworkCore
{
    public class HotelMarriotterDbContext : AbpZeroDbContext<Tenant, Role, User, HotelMarriotterDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<TypeRoom> TypeRoom { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<RegisForm> RegisForm { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingRegisForm> BookingRegisForm { get; set; }
        public DbSet<BookingService> BookingService { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Guest> Guest { get; set; }
        public DbSet<Service> Service { get; set; }

        public HotelMarriotterDbContext(DbContextOptions<HotelMarriotterDbContext> options)
            : base(options)
        {
        }
    }
}
