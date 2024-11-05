using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Domain.Models
{
    public class WorkSchedule
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }  // Id pracownika
        public DateTime WorkDate { get; set; }  // Data pracy
        public TimeSpan StartTime { get; set; }  // Godzina rozpoczęcia pracy
        public TimeSpan EndTime { get; set; }  // Godzina zakończenia pracy
        public virtual Employee Employee { get; set; }  // Powiązanie z pracownikiem

    }
}
