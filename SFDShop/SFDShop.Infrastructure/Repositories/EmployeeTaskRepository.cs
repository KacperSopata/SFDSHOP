using SFDShop.Domain.Interface;
using SFDShop.Domain.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SFDShop.Infrastructure.Repositories
{
    public class EmployeeTaskRepository : IEmployeeTaskRepository
    {
        private readonly SFDShopDbContext _context;

        public EmployeeTaskRepository(SFDShopDbContext context)
        {
            _context = context;
        }

        public IQueryable<EmployeeTask> GetByEmployeeId(int employeeId)
        {
            return _context.EmployeeTasks.Where(t => t.EmployeeId == employeeId).AsQueryable();
        }

        public EmployeeTask GetById(int taskId)
        {
            return _context.EmployeeTasks.Find(taskId);
        }

        public void AddTask(EmployeeTask task)
        {
            _context.EmployeeTasks.Add(task);
            _context.SaveChanges();
        }

        public void Update(EmployeeTask task)
        {
            _context.EmployeeTasks.Update(task);
            _context.SaveChanges();
        }

        public void Delete(int taskId)
        {
            var task = _context.EmployeeTasks.Find(taskId);
            if (task != null)
            {
                _context.EmployeeTasks.Remove(task);
                _context.SaveChanges();
            }
        }
        public IQueryable<EmployeeTask> GetAll()
        {
            return _context.EmployeeTasks;  // Zwracamy IQueryable bez ToList()
        }
    }
}
