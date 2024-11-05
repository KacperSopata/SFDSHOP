using SFDShop.Application.ViewModel.Employee;
using SFDShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Domain.Interface
{
    public interface IEmployeeService
    {
        public ListEmployeeForList GetAllEmployeesForList(int pageSize, int pageNo, string searchString);
        EmployeeDetailsVm GetEmployeeDetails(int id);
        NewEmployeeVm GetEmployeeForEdit(int id);
        int AddEmployee(NewEmployeeVm model);
        void UpdateEmployee(NewEmployeeVm model);
        void DeleteEmployee(int id);
        List<Employee> GetAllEmployees(); // Dodaj tę metodę
        Employee GetEmployeeById(int employeeId); // Dodaj tę metodę do interfejsu
    }
}
