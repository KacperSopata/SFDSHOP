using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SFDShop.Application.Interfaces;
using SFDShop.Application.Services;
using SFDShop.Application.ViewModel.Customer;
using SFDShop.Domain.Interface;
using SFDShop.Application.Interfaces;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SFDShop.Application.ViewModel.Customer.NewCustomerVm;
using SFDShop.Application.PDFGenerator;

namespace SFDShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IValidator<NewCustomerVm>, NewCustomerValidator>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPdfGeneratorService, PdfGeneratorService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmployeeTaskService, EmployeeTaskService>();
            services.AddTransient<IWorkScheduleService, WorkScheduleService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
