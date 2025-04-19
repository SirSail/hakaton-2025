using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SystemUser> SystemUsers { get; set; } 
        public DbSet<PatientInfo> PatientInfos { get; set; }
        public DbSet<CalendarItem> CalendarItems { get; set; }
    }

}