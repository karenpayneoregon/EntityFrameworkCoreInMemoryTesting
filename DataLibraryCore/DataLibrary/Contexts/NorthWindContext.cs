using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ConfigurationHelper;
using DataLibraryCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DataLibraryCore.Contexts
{
    public partial class NorthWindContext : DbContext
    {
        private readonly StreamWriter _logStream = new("logging.txt", append: true);


        public NorthWindContext()
        {
        }

        public NorthWindContext(DbContextOptions<NorthWindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ContactContactDevices> ContactContactDevices { get; set; }
        public virtual DbSet<ContactType> ContactType { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<PhoneType> PhoneType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                var serverName = Environment.UserName.ToLower() == "paynek" ? ".\\SQLEXPRESS" : "KARENS-PC";
                var connectionString = $"Server={serverName};Database=NorthWindAzure2;Trusted_Connection=True;";



#if (EFC_LOG_ENABLED_InMemory)
                //optionsBuilder.UseSqlServer($"Server={serverName};Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
                //optionsBuilder.UseLoggerFactory(GetLoggerFactory()).EnableSensitiveDataLogging().UseSqlServer(connectionString);
                optionsBuilder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

#else
                optionsBuilder.UseSqlServer(Helper.ConnectionString());
#endif
            }

        }

        private static void LogQueryInfoToDebugOutputWindow(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Helper.ConnectionString())
                .EnableSensitiveDataLogging()
                .LogTo(message => Debug.WriteLine(message));
        }

        private void LogQueryInfoToFile(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Helper.ConnectionString())
                .EnableSensitiveDataLogging()
                .LogTo(message => _logStream.WriteLine(message),
                    LogLevel.Information,
                    DbContextLoggerOptions.Category);
        }
        private static void NoLogging(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Helper.ConnectionString());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.InUse).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ContactContactDevices>(entity =>
            {
                entity.HasOne(d => d.ContactIdentifierNavigation)
                    .WithMany(p => p.ContactContactDevices)
                    .HasForeignKey(d => d.ContactIdentifier)
                    .HasConstraintName("FK_ContactContactDevices_Contact");

                entity.HasOne(d => d.PhoneTypeIdenitfierNavigation)
                    .WithMany(p => p.ContactContactDevices)
                    .HasForeignKey(d => d.PhoneTypeIdenitfier)
                    .HasConstraintName("FK_ContactContactDevices_PhoneType");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerIdentifier)
                    .HasName("PK_Customers_1");

                entity.Property(e => e.InUse).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ContactIdentifierNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.ContactIdentifier)
                    .HasConstraintName("FK_Customers_Contact");

                entity.HasOne(d => d.ContactTypeIdentifierNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.ContactTypeIdentifier)
                    .HasConstraintName("FK_Customers_ContactType");

                entity.HasOne(d => d.CountryIdentfierNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CountryIdentfier)
                    .HasConstraintName("FK_Customers_Countries");
            });
        }
        #region Takes care of disposing stream used for logging
        public override void Dispose()
        {
            base.Dispose();
            _logStream.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await _logStream.DisposeAsync();
        }
        #endregion             
    }
}
