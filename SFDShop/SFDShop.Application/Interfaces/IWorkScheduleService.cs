using SFDShop.Application.ViewModel.WorkSchedule;
using SFDShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.Interfaces
{
    public interface IWorkScheduleService
    {
        void AddWorkSchedule(NewWorkScheduleVm scheduleVm);
        void UpdateWorkSchedule(NewWorkScheduleVm schedule); 
        void DeleteWorkSchedule(int id);
        WorkScheduleDetailsVm GetWorkScheduleById(int id);
        ListWorkScheduleForList GetWorkSchedulesByEmployeeId(int employeeId, int pageSize, int pageNo); 
        ListWorkScheduleForList GetAllWorkSchedulesForList(int pageSize, int pageNo, string searchString);
        List<EmployeeWorkScheduleViewModel> GetWorkSchedulesGroupedByEmployee(string searchString);

    }
}
