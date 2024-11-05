using SFDShop.Domain.Interface;
using SFDShop.Domain.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SFDShop.Infrastructure.Repositories
{
    public class WorkScheduleRepository : IWorkScheduleRepository
    {
        private readonly SFDShopDbContext _context;
        public WorkScheduleRepository(SFDShopDbContext context)
        {
            _context = context;
        }

        // Dodawanie nowego grafiku
        public void Add(WorkSchedule schedule)
        {
            _context.WorkSchedules.Add(schedule);
            _context.SaveChanges();  // Zapis do bazy danych
        }

        // Aktualizacja istniejącego grafiku
        public void Update(WorkSchedule schedule)
        {
            _context.WorkSchedules.Update(schedule);
            _context.SaveChanges();  // Zapis do bazy danych
        }

        // Usuwanie grafiku na podstawie Id
        public void Delete(int id)
        {
            var schedule = _context.WorkSchedules.Find(id);
            if (schedule != null)
            {
                _context.WorkSchedules.Remove(schedule);
                _context.SaveChanges();  // Zapis zmian do bazy danych
            }
        }

        public WorkSchedule GetById(int id)
        {
            return _context.WorkSchedules
                           .Include(s => s.Employee)  // Ładowanie danych o pracowniku
                           .FirstOrDefault(s => s.Id == id);  // Pobieranie konkretnego grafiku
        }

        public IQueryable<WorkSchedule> GetAll()
        {
            return _context.WorkSchedules.Include(s => s.Employee);  // Zwraca grafiki z danymi o pracownikach
        }

        public IQueryable<WorkSchedule> GetByEmployeeId(int employeeId)
        {
            return _context.WorkSchedules
                           .Where(s => s.EmployeeId == employeeId)
                           .Include(s => s.Employee);  // Zwraca grafiki z danymi o pracownikach
        }
        public int GetCount()
        {
            return _context.WorkSchedules.Count();
        }
    }
}
