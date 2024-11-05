using Microsoft.Extensions.DependencyInjection;
using SFDShop.Domain.Interface;
using SFDShop.Infrastructure.Repositories;
using SuplementShopWEB.MVC.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IOrderRepository,OrderRepository>();
            services.AddTransient<ICustomerRepository,CustomerRepository>();
            services.AddTransient<IItemRepository,ItemRepository>();
            services.AddTransient<IUserRepository,UserRepository>();
            services.AddTransient<IEmployeeRepository,EmployeeRepository>();
            services.AddTransient<IEmployeeTaskRepository,EmployeeTaskRepository>();
            services.AddTransient<IWorkScheduleRepository,WorkScheduleRepository>();
            return services;
        }
    }
}
