using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.EmployeeTask
{
    public class ListEmployeeTaskForList
    {
        public List<EmployeeTaskForListVm> Tasks { get; set; } // Lista zadań
        public int CurrentlyPage { get; set; } // Aktualna strona (do paginacji)
        public int PageSize { get; set; } // Rozmiar strony (liczba zadań na stronę)
        public string SearchString { get; set; } // Fraza do wyszukiwania zadań
        public int Count { get; set; } // Liczba wszystkich zadań (do paginacji)
    }
}
