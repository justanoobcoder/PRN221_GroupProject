using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects;

public class LaundryMiddlePlatformDbContext : DbContext
{
    public LaundryMiddlePlatformDbContext()
    {
    }

    public LaundryMiddlePlatformDbContext(DbContextOptions<LaundryMiddlePlatformDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Customer> Customers { get; set; } = null!;
    public virtual DbSet<Machine> Machines { get; set; } = null!;
    public virtual DbSet<Store> Stores { get; set; } = null!;
    public virtual DbSet<Service> Services { get; set; } = null!;
    public virtual DbSet<Order> Orders { get; set; } = null!;
    public virtual DbSet<MachineOrderAssignment> MachineOrderAssignments { get; set; } = null!;

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        return config["ConnectionStrings:DB"]!;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("server=(local);database=LaundryMiddlePlatform;uid=sa;pwd=12345;TrustServerCertificate=True");
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity(CustomerTableConfiguration());
        modelBuilder.Entity(MachineTableConfiguration());
        modelBuilder.Entity(ServiceTableConfiguration());
        modelBuilder.Entity(StoreTableConfiguration());
        modelBuilder.Entity(OrderTableConfiguration());
        modelBuilder.Entity(MachineOrderAssignmentTableConfiguration());
    }

    private static Action<EntityTypeBuilder<Customer>> CustomerTableConfiguration()
    {
        return entity =>
        {
            entity.ToTable("Customer");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

            entity.Property(e => e.FullName)
            .HasColumnName("FullName")
            .HasMaxLength(100)
            .IsRequired();

            entity.Property(e => e.Address)
            .HasColumnName("Address")
            .HasMaxLength(200)
            .IsRequired();

            entity.Property(e => e.Phone)
            .HasColumnName("Phone")
            .HasMaxLength(20)
            .IsUnicode(false)
            .IsRequired();

            entity.Property(e => e.Password)
            .HasColumnName("Password")
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

            entity.Property(e => e.Email)
            .HasColumnName("Email")
            .HasMaxLength(30)
            .IsUnicode(false);

            entity.Property(e => e.AvatarUrl)
            .HasColumnName("AvatarUrl")
            .HasMaxLength(200)
            .IsUnicode(false);

            entity.Property(e => e.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsBanned)
            .HasColumnName("IsBanned")
            .HasColumnType("bit")
            .HasDefaultValueSql("((0))");

            entity
            .HasMany(e => e.Orders)
            .WithOne(e => e.Customer)
            .HasForeignKey(e => e.CustomerId);
        };
    }

    private static Action<EntityTypeBuilder<Machine>> MachineTableConfiguration()
    {
        return entity =>
        {
            entity.ToTable("Machine");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
            .HasColumnName("Name")
            .HasMaxLength(100)
            .IsRequired();

            entity.Property(e => e.Description)
            .HasColumnName("Description")
            .HasMaxLength(200);

            entity.Property(e => e.Brand)
            .HasColumnName("Brand")
            .HasMaxLength(100)
            .IsRequired();

            entity.Property(e => e.Model)
            .HasColumnName("Model")
            .HasMaxLength(100)
            .IsRequired();

            entity.Property(e => e.Capacity)
            .HasColumnName("Capacity")
            .IsRequired();

            entity.Property(e => e.IsAvailable)
            .HasColumnName("IsAvailable")
            .HasColumnType("bit")
            .HasDefaultValueSql("((1))");

            entity.Property(e => e.WashTimeInMinute)
            .HasColumnName("WashTimeInMinute")
            .IsRequired();

            entity.Property(e => e.StoreId)
            .HasColumnName("StoreId")
            .IsRequired();

            entity.Property(e => e.DeletedAt)
            .HasColumnName("DeletedAt")
            .HasColumnType("datetime");
        };
    }

    private static Action<EntityTypeBuilder<MachineOrderAssignment>> MachineOrderAssignmentTableConfiguration()
    {
        return entity =>
        {
            entity.ToTable("MachineOrderAssignment");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

            entity.Property(e => e.MachineId)
            .HasColumnName("MachineId")
            .IsRequired();

            entity.Property(e => e.OrderId)
            .HasColumnName("OrderId")
            .IsUnicode(false)
            .HasMaxLength(20)
            .IsRequired();

            entity.Property(e => e.AssignedStartAt)
            .HasColumnName("AssignedStartAt")
            .HasColumnType("datetime")
            .IsRequired();

            entity.Property(e => e.AssignedEndAt)
            .HasColumnName("AssignedEndAt")
            .HasColumnType("datetime")
            .IsRequired();

            entity
            .HasOne(e => e.Machine)
            .WithMany()
            .HasForeignKey(e => e.MachineId);

            entity
            .HasOne(e => e.Order)
            .WithOne()
            .HasForeignKey<MachineOrderAssignment>(e => e.OrderId)
            .OnDelete(DeleteBehavior.NoAction);
        };
    }

    private static Action<EntityTypeBuilder<Service>> ServiceTableConfiguration()
    {
        return entity =>
        {
            entity.ToTable("Service");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
            .HasColumnName("Name")
            .HasMaxLength(100)
            .IsRequired();

            entity.Property(e => e.Description)
            .HasColumnName("Description")
            .HasMaxLength(200);

            entity.Property(e => e.PricePerKg)
            .HasColumnName("PricePerKg")
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

            entity.Property(e => e.ServiceTimeInHour)
            .HasColumnName("ServiceTimeInHour")
            .IsRequired();

            entity.Property(e => e.StoreId)
            .HasColumnName("StoreId")
            .IsRequired();

            entity.Property(e => e.DeletedAt)
            .HasColumnName("DeletedAt")
            .HasColumnType("datetime");
        };
    }

    private static Action<EntityTypeBuilder<Store>> StoreTableConfiguration()
    {
        return entity =>
        {
            entity.ToTable("Store");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
            .HasColumnName("Name")
            .HasMaxLength(100)
            .IsRequired();

            entity.Property(e => e.Address)
            .HasColumnName("Address")
            .HasMaxLength(200)
            .IsRequired();

            entity.Property(e => e.Phone)
            .HasColumnName("Phone")
            .HasMaxLength(20)
            .IsUnicode(false)
            .IsRequired();

            entity.Property(e => e.Password)
            .HasColumnName("Password")
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

            entity.Property(e => e.Email)
            .HasColumnName("Email")
            .HasMaxLength(30)
            .IsUnicode(false);

            entity.Property(e => e.Description)
            .HasColumnName("Description")
            .HasMaxLength(200)
            .IsRequired();

            entity.Property(e => e.AvatarUrl)
            .HasColumnName("AvatarUrl")
            .HasMaxLength(200)
            .IsUnicode(false);

            entity.Property(e => e.CoverUrl)
            .HasColumnName("CoverUrl")
            .HasMaxLength(200)
            .IsUnicode(false);

            entity.Property(e => e.FacebookUrl)
            .HasColumnName("FacebookUrl")
            .HasMaxLength(200)
            .IsUnicode(false);

            entity.Property(e => e.OpenTime)
            .HasColumnName("OpenTime")
            .HasColumnType("time(0)");

            entity.Property(e => e.CloseTime)
            .HasColumnName("CloseTime")
            .HasColumnType("time(0)");

            entity.Property(e => e.IsOpening)
            .HasColumnName("IsOpening")
            .HasColumnType("bit")
            .HasDefaultValueSql("((0))");

            entity.Property(e => e.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsBanned)
            .HasColumnName("IsBanned")
            .HasColumnType("bit")
            .HasDefaultValueSql("((0))");

            entity
            .HasMany(e => e.Machines)
            .WithOne()
            .HasForeignKey(e => e.StoreId);

            entity
            .HasMany(e => e.Services)
            .WithOne()
            .HasForeignKey(e => e.StoreId);
        };
    }
    private static Action<EntityTypeBuilder<Order>> OrderTableConfiguration()
    {
        return entity =>
        {
            entity.ToTable("Order");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
            .HasColumnName("Id")
            .IsUnicode(false)
            .HasMaxLength(20);

            entity.Property(e => e.CustomerId)
            .HasColumnName("CustomerId")
            .IsRequired();

            entity.Property(e => e.ServiceId)
            .HasColumnName("ServiceId")
            .IsRequired();

            entity.Property(e => e.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.PickUpAt)
            .HasColumnName("PickUpAt")
            .HasColumnType("datetime")
            .IsRequired(false);

            entity.Property(e => e.Status)
            .HasColumnName("Status")
            .HasMaxLength(20)
            .IsUnicode(false)
            .IsRequired();

            entity.Property(e => e.Note)
            .HasColumnName("Note")
            .HasMaxLength(200)
            .IsRequired(false);

            entity.Property(e => e.DropOffAt)
            .HasColumnName("DropOffAt")
            .HasColumnType("datetime")
            .IsRequired(false);

            entity.Property(e => e.Weight)
            .HasColumnName("Weight")
            .IsRequired();

            entity.Property(e => e.TotalCost)
            .HasColumnName("TotalCost")
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

            entity
            .HasOne(e => e.Service)
            .WithMany()
            .HasForeignKey(e => e.ServiceId);
        };
    }
}
