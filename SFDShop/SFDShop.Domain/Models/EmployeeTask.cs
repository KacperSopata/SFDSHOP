using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Domain.Models
{
    public class EmployeeTask
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public DateTime TaskDate { get; set; }
        public bool IsCompleted { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}