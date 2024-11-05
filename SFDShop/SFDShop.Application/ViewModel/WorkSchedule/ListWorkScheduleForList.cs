using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.WorkSchedule
{
    public class ListWorkScheduleForList
    {
        public List<WorkScheduleForListVm> WorkSchedules { get; set; } // Użyj tej właściwości, jeśli to jest to, co chcesz

        public int CurrentlyPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }

        public ListWorkScheduleForList()
        {
            WorkSchedules = new List<WorkScheduleForListVm>(); // Inicjalizuj listę
        }
    }
}