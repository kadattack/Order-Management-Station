using Metronik.DTOs;
using Metronik.Enteties;
using Metronik.Entities;
using Microsoft.EntityFrameworkCore;

namespace Metronik.Data;

// public class DataContext : DbContext
public class DataContext : DbContext

{
    public DataContext(DbContextOptions options) : base(options)
    {  }

    // public DbSet<AppUsers> Users { get; set; }
    public DbSet<AppOrderDetails> OrderDetails { get; set; }
    public DbSet<AppOrderProduct> OrderProduct { get; set; }
    public DbSet<AppOms> Oms { get; set; }

    public DbSet<AppOrder> Orders { get; set; }
    public DbSet<AppSerialNumbers> SerialNumbers { get; set; }



    // public DbSet<AppRoomsAppUsers> AppRoomsAppUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //one-to-many
        modelBuilder.Entity<AppSerialNumbers>()
            .HasOne<AppOrderProduct>(s => s.AppOrderProduct)
            .WithMany(s => s.SerialNumbers)
            .HasForeignKey(s => s.AppOrderProductId);



        // modelBuilder.Entity<AppOms>().HasOne<AppUserPass>(x => x.AppUserPass)
            // .WithMany(x => x.OmsList).HasForeignKey(x => x.AppUserPassId);


        // modelBuilder.Entity<AppOrderProduct>().HasOne<AppOms>(x => x.appOms)
        //     .WithMany(x => x.AppOrderProducts).HasForeignKey(x => x.omsTableId);
        //
        // modelBuilder.Entity<AppOms>().HasOne(x => x.AppOrderDetails).WithOne(x => x.appOms)
        //     .HasForeignKey<AppOrderDetails>(x=>x.Id);

    }


    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //
    //     modelBuilder.Entity<AppRooms>()
    //         .HasOne<AppUsers>(x=>x.Host)
    //         .WithMany(g => g.HostOfRooms)
    //         .HasForeignKey(s => s.HostId);
    //
    //
    // }
}