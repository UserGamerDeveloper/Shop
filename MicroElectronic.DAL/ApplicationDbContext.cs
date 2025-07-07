using MicroElectronic.Domain.Enum;
using MicroElectronic.Domain.Helpers;
using MicroElectronic.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroElectronic.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<ApplicationItem> ApplicationItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);

                builder.HasData(new User[]
                {
                    new User()
                    {
                        Id = 1,
                        Name = "Дмитрий",
                        Surname = "Зотов",
                        Position = "Developer",
                        Login = "admin",
                        Password = HashPasswordHelper.HashPassword("admin"),
                        Role = Role.Admin
                    }
                });

                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
                builder.Property(x => x.Surname).HasMaxLength(50).IsRequired();
                builder.Property(x => x.Login).IsRequired();
                builder.Property(x => x.Password).IsRequired();
            });

            modelBuilder.Entity<Category>(builder =>
            {
                builder.ToTable("Categories").HasKey(x => x.Id);

                builder.HasData(new Category[]
                {
                    new Category()
                    {
                        Id=1,
                        Name="Кристальное производство",
                        ImageUrl = "/lib/images/crystal.png"
                    }
                });

                builder.Property(x=> x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Name).HasMaxLength(60).IsRequired();

                builder.Property(x => x.ImageUrl).HasMaxLength(200);
            });

            modelBuilder.Entity<Equipment>(builder =>
            {
                builder.ToTable("Equipments").HasKey(x => x.Id);

                builder.HasData(new Equipment[]
                {
                    //new Equipment()
                    //{
                    //    Id = 1,
                    //    CategoryId = 1,
                    //    Name = "UNIXX S760+",
                    //    Description = "Полуавтоматическая установка нанесения покрытий методом центрифугирования",
                    //    Price = 450000,
                    //    Size = "1250 х 1250/1510 х 2000/2500 мм",
                    //    BodyMaterial = "Изготовлен из нержавеющей стали",
                    //    WorkingArea = "500 x 750 мм",
                    //    //Power = "400 (208) В, 3 фазы, 50-60 Гц",
                    //    GuaranteePeriod = "2 года",
                    //    FullDescription = "Полуавтоматическая автономная установка UNIXX S 760+ предназначена для нанесения покрытий методом " +
                    //    "центрифугирования на крупноразмерные подложки. В установке применяется технология " +
                    //    "CCP (Covered Chuck Processor – центрифугирование с закрытой крышкой), позволяющей наносить покрытия с превосходной " +
                    //    "однородностью и повторяемостью.\r\nПрограммное обеспечение установки имеет дружественный пользовательский " +
                    //    "интерфейс со всеми необходимыми функциями, такими как создание и редактирование рабочих программ, администрирование " +
                    //    "пользователей, отслеживание состояния системы. Подвод необходимых сред (воздух, азот, вакуум) выполняется подключением " +
                    //    "через быстроразъемные соединения и управляется программным обеспечением.",
                    //    ImageUrl = "/lib/images/equipments/poluavtomaticheskaya_ustanovka_naneseniya_pokrytij_metodom_centrifugirovaniya_unixx_s760.jpg"

                    //}
                });

                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.HasOne(u => u.Category)
                    .WithMany(x => x.Equipments)
                    .HasForeignKey(u => u.CategoryId);

                builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
                builder.Property(x => x.Description).HasMaxLength(200);
                builder.Property(x => x.Price).IsRequired();
                builder.Property(x => x.FullDescription).HasMaxLength(3000);
            });

            modelBuilder.Entity<ApplicationItem>(builder =>
            {
                builder.ToTable("ApplicationItems").HasKey(x => x.Id);

                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.HasOne(u => u.User)
                    .WithMany(x => x.ApplicationItems)
                    .HasForeignKey(u => u.UserId);
            });

            modelBuilder.Entity<Order>(builder =>
            {
                builder.ToTable("Orders").HasKey(x => x.Id);

                builder.HasMany(o => o.OrderDetails)
                    .WithOne(od => od.Order)
                    .HasForeignKey(o => o.OrderId);

                builder.HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId);
            });

            modelBuilder.Entity<OrderDetail>(builder =>
            {
                builder.ToTable("OrderDetails").HasKey(od => od.Id);

                builder.Property(od => od.Id).ValueGeneratedOnAdd();
            });
        }
    }
}
