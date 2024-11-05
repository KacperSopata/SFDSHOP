using SFDShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Domain.Interface
{
    public interface IEmployeeTaskRepository
    {
        IQueryable<EmployeeTask> GetByEmployeeId(int employeeId);
        EmployeeTask GetById(int taskId);
        void AddTask(EmployeeTask task);
        void Update(EmployeeTask task);
        void Delete(int taskId);
        IQueryable<EmployeeTask> GetAll();  // Deklaracja metody w interfejsie
    }
}
