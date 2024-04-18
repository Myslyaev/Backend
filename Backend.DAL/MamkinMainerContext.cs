using Backend.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Backend.DAL;

public class MamkinMainerContext : DbContext
{
    public DbSet<UserDto> Users { get; set; }
    public DbSet<DeviceDto> Devices { get; set; }

    public MamkinMainerContext(DbContextOptions<MamkinMainerContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<DeviceDto>()
            .HasOne(d => d.Owner)
            .WithMany(u => u.Devices);
    }
}
