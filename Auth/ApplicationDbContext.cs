using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EasyOverTime.Models.Form;

namespace EasyOverTime.Auth
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<OvertimeData>().ToTable("OvertimeDatas");
            builder.Entity<OvertimeData>(entity =>
			{
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Date);
                entity.Property(e => e.TimeStart);
                entity.Property(e => e.TimeEnd);
                entity.Property(e => e.TotalNumberOfHours);
                entity.Property(e => e.Reason);
                entity.HasOne(e => e.OvertimeForm).WithMany(p => p.OvertimeData);
            });

            //builder.Entity<OvertimeFormModel>().ToTable("OvertimeForms");
            builder.Entity<OvertimeFormModel>(entity =>
			{
                entity.HasKey(e => e.IdNumber);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Position).IsRequired();
                entity.Property(e => e.Branch).IsRequired();
                entity.Property(e => e.DateFiled).IsRequired();
                entity.Property(e => e.Specification).IsRequired();
                entity.Property(e => e.Total);
            });
        }

		public virtual DbSet<OvertimeData> OvertimeDatas { get; set; }
        public virtual DbSet<OvertimeFormModel> OvertimeForms { get; set; }
	}
}
