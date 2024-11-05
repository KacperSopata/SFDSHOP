using SFDShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Domain.Interface
{
    public interface IWorkScheduleRepository
    {
        void Add(WorkSchedule schedule); // Dodawanie grafiku
        void Update(WorkSchedule schedule); // Aktualizacja grafiku
        void Delete(int id); // Usuwanie grafiku
        int GetCount();
        WorkSchedule GetById(int id); // Pobieranie grafiku po Id
        IQueryable<WorkSchedule> GetAll(); // Pobieranie wszystkich grafików
        IQueryable<WorkSchedule> GetByEmployeeId(int employeeId); // Pobieranie grafików po Id pracownika
    }
}
