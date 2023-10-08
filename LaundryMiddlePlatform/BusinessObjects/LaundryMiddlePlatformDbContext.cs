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
    public virtual DbSet<WashingMachine> WashingMachines { get; set; } = null!;
    public virtual DbSet<Store> Stores { get; set; } = null!;
    public virtual DbSet<Service> Services { get; set; } = null!;
    public virtual DbSet<ItemType> ItemTypes { get; set; } = null!;
    public virtual DbSet<ServicePrice> ServicePrices { get; set; } = null!;
    public virtual DbSet<Order> Orders { get; set; } = null!;

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
        //optionsBuilder.UseSqlServer("server=(local);database=LaundryMiddlePlatform;uid=sa;pwd=123456;TrustServerCertificate=True");
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity(CustomerTableConfiguration());
        modelBuilder.Entity(WashingMachineTableConfiguration());
        modelBuilder.Entity(ItemTypeTableConfiguration());
        modelBuilder.Entity(ServiceTableConfiguration());
        modelBuilder.Entity(ServicePriceTableConfiguration());
        modelBuilder.Entity(StoreTableConfiguration());
        modelBuilder.Entity(OrderTableConfiguration());
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

            entity.Property(e => e.DeletedAt)
            .HasColumnName("DeletedAt")
            .HasColumnType("datetime");

            entity
            .HasMany(e => e.Orders)
            .WithOne(e => e.Customer)
            .HasForeignKey(e => e.CustomerId);
        };
    }

    private static Action<EntityTypeBuilder<WashingMachine>> WashingMachineTableConfiguration()
    {
        return entity =>
        {
            entity.ToTable("WashingMachine");
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

            entity.Property(e => e.MaxWeight)
            .HasColumnName("MaxWeight")
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

            entity.Property(e => e.IsAvailable)
            .HasColumnName("IsAvailable")
            .HasColumnType("bit")
            .HasDefaultValueSql("((1))");

            entity.Property(e => e.StoreId)
            .HasColumnName("StoreId")
            .IsRequired();
        };
    }

    private static Action<EntityTypeBuilder<ItemType>> ItemTypeTableConfiguration()
    {
        return entity =>
        {
            entity.ToTable("ItemType");
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

            entity.Property(e => e.ItemTypeId)
            .HasColumnName("ItemTypeId")
            .IsRequired();

            entity.Property(e => e.StoreId)
            .HasColumnName("StoreId")
            .IsRequired();

            entity
            .HasOne(e => e.ItemType)
            .WithMany()
            .HasForeignKey(e => e.ItemTypeId);

            entity
            .HasMany(e => e.ServicePrices)
            .WithOne()
            .HasForeignKey(e => e.ServiceId);
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

            entity.Property(e => e.LogoUrl)
            .HasColumnName("LogoUrl")
            .HasMaxLength(200)
            .IsUnicode(false);

            entity.Property(e => e.FacebookUrl)
            .HasColumnName("FacebookUrl")
            .HasMaxLength(200)
            .IsUnicode(false);

            entity.Property(e => e.OwnerName)
            .HasColumnName("OwnerName")
            .HasMaxLength(100)
            .IsRequired();

            entity.Property(e => e.OpenTime)
            .HasColumnName("OpenTime")
            .HasColumnType("time(0)");

            entity.Property(e => e.CloseTime)
            .HasColumnName("CloseTime")
            .HasColumnType("time(0)");

            entity.Property(e => e.IsOpened)
            .HasColumnName("IsOpened")
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

            entity.Property(e => e.DeletedAt)
            .HasColumnName("DeletedAt")
            .HasColumnType("datetime");

            entity
            .HasMany(e => e.WashingMachines)
            .WithOne()
            .HasForeignKey(e => e.StoreId);

            entity
            .HasMany(e => e.Services)
            .WithOne()
            .HasForeignKey(e => e.StoreId);

            entity
            .HasMany(e => e.Orders)
            .WithOne(e => e.Store)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(e => e.StoreId);
        };
    }

    private static Action<EntityTypeBuilder<ServicePrice>> ServicePriceTableConfiguration()
    {
        return entity =>
        {
            entity.ToTable("ServicePrice");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

            entity.Property(e => e.MinWeight)
            .HasColumnName("MinWeight")
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

            entity.Property(e => e.MaxWeight)
            .HasColumnName("MaxWeight")
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

            entity.Property(e => e.Price)
            .HasColumnName("Price")
            .HasColumnType("decimal(18, 2)")
            .IsRequired(false);

            entity.Property(e => e.PricePerUnit)
            .HasColumnName("PricePerUnit")
            .HasColumnType("decimal(18, 2)")
            .IsRequired(false);

            entity.Property(e => e.WashTimeInMinute)
            .HasColumnName("WashTimeInMinute")
            .HasColumnType("int")
            .IsRequired();

            entity.Property(e => e.ServiceId)
            .HasColumnName("ServiceId")
            .IsRequired();
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

            entity.Property(e => e.StoreId)
            .HasColumnName("StoreId")
            .IsRequired();

            entity.Property(e => e.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.FinishedAt)
            .HasColumnName("FinishedAt")
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

            entity.Property(e => e.TakenAt)
            .HasColumnName("TakenAt")
            .HasColumnType("datetime")
            .IsRequired(false);

            entity.Property(e => e.Weight)
            .HasColumnName("Weight")
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

            entity.Property(e => e.TotalPrice)
            .HasColumnName("TotalPrice")
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

            entity.Property(e => e.ServicePriceId)
            .HasColumnName("ServicePriceId")
            .IsRequired();

            entity
            .HasOne(e => e.ServicePrice)
            .WithMany()
            .HasForeignKey(e => e.ServicePriceId);
        };
    }
}
