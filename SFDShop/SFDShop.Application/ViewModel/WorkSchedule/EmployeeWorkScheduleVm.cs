using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.WorkSchedule
{
    public class EmployeeWorkScheduleViewModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeFullName { get; set; } // Imię i nazwisko pracownika
        public List<WorkDay> WorkDays { get; set; } // Lista dni roboczych dla pracownika
        public EmployeeWorkScheduleViewModel()
        {
            WorkDays = new List<WorkDay>(); // Inicjalizuj listę dni roboczych
        }
    }
}
