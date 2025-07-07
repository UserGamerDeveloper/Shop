using MicroElectronic.DAL.Interfaces;
using MicroElectronic.DAL.Repositories;
using MicroElectronic.Domain.Models;
using MicroElectronic.Service.Implementations;
using MicroElectronic.Service.Interfaces;

namespace MicroElectronic
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<Category>, CategoryRepository>();
            services.AddScoped<IBaseRepository<Equipment>, EquipmentRepository>();
            services.AddScoped<IBaseRepository<ApplicationItem>, ApplicationItemsRepository>();
            services.AddScoped<IBaseRepository<Order>, OrderRepository>();
            services.AddScoped<IBaseRepository<OrderDetail>, OrderDetailRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IBufferedFileUploadService, BufferedFileUploadService>();
            services.AddScoped<IApplicationItemService, ApplicationItemService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
