using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.Employee
{
    public class ListEmployeeForList
    {
        public List<EmployeeForListVm> Employees { get; set; }
        public int CurrentlyPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
    }
}