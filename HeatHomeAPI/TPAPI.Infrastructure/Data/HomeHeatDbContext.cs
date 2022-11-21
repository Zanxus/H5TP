using Microsoft.EntityFrameworkCore;
using TPAPI.Domain.Entities;

namespace TPAPI.Infrastructure
{
    public class HomeHeatDbContext : DbContext
    {
        public HomeHeatDbContext(DbContextOptions<HomeHeatDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }


        public DbSet<GeoCoordinate> Coordinates { get; set; }
        public DbSet<Temperature> Temperature { get; set; }
        public DbSet<HeatingUnit> HeatingUnits { get; set; }
        //public DbSet<HardwareUnit> HardwareUnits { get; set; }
        //public DbSet<HardwareType> HardwareTypes { get; set; }
    }
}
