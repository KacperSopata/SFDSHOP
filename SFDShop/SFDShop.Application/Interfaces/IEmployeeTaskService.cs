using SFDShop.Application.ViewModel.EmployeeTask;
using SFDShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Domain.Interface
{
    public interface IEmployeeTaskService
    {
        public ListEmployeeTaskForList GetTasksForList(int pageSize, int pageNo, string searchString);
        EmployeeTaskDetailsVm GetTaskDetails(int taskId);
        NewEmployeeTaskVm GetTaskForEdit(int taskId);
        void AddTask(EmployeeTask model);
        void UpdateTask(NewEmployeeTaskVm model);
        int DeleteTask(int taskId);
    }
}
