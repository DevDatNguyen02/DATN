using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace HotelMarriotter.EntityFrameworkCore
{
    public static class HotelMarriotterDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<HotelMarriotterDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<HotelMarriotterDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
