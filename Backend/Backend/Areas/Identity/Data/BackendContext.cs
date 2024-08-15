using Backend.Areas.Identity.Data;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Backend.Areas.Identity.Data;

public class BackendContext : IdentityDbContext<BackendUser>
{
    public BackendContext(DbContextOptions<BackendContext> options)
        : base(options)
    {
    }

    public object TaskList { get; internal set; }
    public DbSet<TaskList> UserTasks { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new BackendUserEntityConfiguration());
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
       builder.Entity<BackendUser>()
                    .HasMany(u => u.TaskLists)
                    .WithOne(t => t.User)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

    }

}

public class BackendUserEntityConfiguration : IEntityTypeConfiguration<BackendUser>
{
    public void Configure(EntityTypeBuilder<BackendUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(255);
        builder.Property(x => x.LastName).HasMaxLength(255);
    }
}