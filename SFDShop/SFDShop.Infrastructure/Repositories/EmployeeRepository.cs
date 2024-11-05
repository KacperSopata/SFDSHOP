using SFDShop.Domain.Interface;
using SFDShop.Domain.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SFDShop.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SFDShopDbContext _context;

        public EmployeeRepository(SFDShopDbContext context)
        {
            _context = context;
        }

        public IQueryable<Employee> GetAll()
        {
            // Zwracamy IQueryable, aby móc później filtrować
            return _context.Employees.AsQueryable();
        }

        public Employee GetById(int id)
        {
            // Używamy Include, aby pobrać pracownika wraz z jego zadaniami
            return _context.Employees.Include(e => e.Tasks).FirstOrDefault(e => e.Id == id);
        }

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }
    }
}
