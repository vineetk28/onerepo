using MantiScanServices.Model.Incidents;
using MantiScanServices.Model.Master;
using MantiScanServices.Model.Modules;
using MantiScanServices.Model.OilSpillReport;
using MantiScanServices.Model.Organizations;
using MantiScanServices.Model.Plateforms;
using MantiScanServices.Model.RolePrivilege;
using MantiScanServices.Model.Roles;
using MantiScanServices.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace MantiScanServices.DataProvider
{
    public class MantiDbContext : IdentityDbContext<User>
    {
        public DbSet<User> User { get; set; }
        public DbSet<RolePrivilege> RolePrivilege { get; set; }
        public DbSet<Role> MantiScanRole { get; set; }
        public DbSet<RoleDetail> RoleDetails { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<UserRoleDetail> UserRoleDetails { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<OilSpillReport> OilSpillReports { get; set; }
        public DbSet<NotificationCompany> NotificationCompanys { get; set; }
        public DbSet<NotificationAgency> NotificationAgencys { get; set; }
        public DbSet<PlateForm> PlateForms { get; set; }
        public DbSet<Tower> Towers { get; set; }

   
        public MantiDbContext(DbContextOptions<MantiDbContext> options) : base(options)
        {
           // Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            var DbConnectionString = configuration["ConnectionStrings:PostgreSqlProviderPath"];

            optionsBuilder.UseNpgsql(DbConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("public");
            
            builder.Entity<User>().ToTable(nameof(User)).HasKey(m => m.Id);
            builder.Entity<RolePrivilege>().ToTable(nameof(RolePrivilege)).HasKey(m => m.RolePrivilegeId);
            builder.Entity<Role>().ToTable("MantiScanRole").HasKey(m => m.RoleId);
            builder.Entity<RoleDetail>().ToTable(nameof(RoleDetail)).HasKey(m => m.RoleDetailId);
            builder.Entity<IdentityRole>().ToTable("IdentityRole").HasKey(m => m.Id);
            builder.Entity<Module>().ToTable(nameof(Module)).HasKey(m => m.ModuleId);
            builder.Entity<Organization>().ToTable(nameof(Organization)).HasKey(m => m.OrganizationId);
            builder.Entity<UserRoleDetail>().ToTable(nameof(UserRoleDetail)).HasKey(m => m.UserRoleDetailId);
            builder.Entity<Incident>().ToTable(nameof(Incident)).HasKey(m => m.IncidentId);
            builder.Entity<OilSpillReport>().ToTable(nameof(OilSpillReport)).HasKey(m => m.OilSpillReportId);
            builder.Entity<NotificationCompany>().ToTable(nameof(NotificationCompany)).HasKey(m => m.NotificationCompanyId);
            builder.Entity<NotificationAgency>().ToTable(nameof(NotificationAgency)).HasKey(m => m.NotificationAgencyId);
            builder.Entity<PlateForm>().ToTable(nameof(PlateForm)).HasKey(m => m.PlateFormId);
            builder.Entity<Tower>().ToTable(nameof(Tower)).HasKey(m => m.TowerId);
            
            base.OnModelCreating(builder);
        }

        private void UpdateUpdatedProperty<T>() where T : class
        {
            var addedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added);

            foreach (var entry in addedSourceInfo)
            {
                entry.Property("DateCreated").CurrentValue = DateTime.UtcNow;
                entry.Property("DateModified").CurrentValue = DateTime.UtcNow;
            }

            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("DateModified").CurrentValue = DateTime.UtcNow;
            }
        }

        public override int SaveChanges()
        {
            //ChangeTracker.DetectChanges();

            UpdateUpdatedProperty<User>();            
            UpdateUpdatedProperty<Organization>();            
            UpdateUpdatedProperty<Incident>();
            UpdateUpdatedProperty<OilSpillReport>();
            UpdateUpdatedProperty<NotificationCompany>();
            UpdateUpdatedProperty<NotificationAgency>();
            UpdateUpdatedProperty<PlateForm>();
            UpdateUpdatedProperty<Tower>();

            return base.SaveChanges();
        }
    }
}
