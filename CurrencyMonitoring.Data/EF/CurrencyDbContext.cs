using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CurrencyMonitoring.Data.Models;
using CurrencyMonitoring.Data.Repository;
using CurrencyMonitoring.Data.Service;
using System.Text.Json;

namespace CurrencyMonitoring.Data.EF
{
    public class CurrencyDbContext : DbContext 
    {
        public CurrencyDbContext():base("CurrencyDbConnctionString")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().Property(x => x.Price).HasPrecision(15, 10);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ChangesAccordingToTime>().Property(x => x.Price).HasPrecision(15, 10);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ChangesAccordingToTime>().Property(x => x.ChangeInPercentAfterLatestUpdate).HasPrecision(15, 10);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PortfolioData>().Property(x => x.Value).HasPrecision(15, 10);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ChangesAccordingToTime> ChangesAccordingToTime { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CurrencyPortfolio> CurrencyPortfolios { get; set; }
        public DbSet<PortfolioData> PortfolioData { get; set; }



        static void Main()
        {

            UserDataProvider user = new UserDataProvider();
            user.AddNewUser("dasgfag", "asdgfasdgdfs");

        }
    }
}
