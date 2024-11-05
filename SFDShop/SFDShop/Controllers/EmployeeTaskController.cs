using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFDShop.Application.Interfaces;
using SFDShop.Application.Services;
using SFDShop.Application.ViewModel.EmployeeTask;
using SFDShop.Domain.Interface;
using SFDShop.Domain.Models;
using System.Linq;

namespace SFDShop.Controllers
{
    public class EmployeeTaskController : Controller
    {
        private readonly IEmployeeTaskService _employeeTaskService;
        private readonly IEmployeeService _employeeService;

        public EmployeeTaskController(IEmployeeTaskService employeeTaskService, IEmployeeService employeeService)
        {
            _employeeTaskService = employeeTaskService;
            _employeeService = employeeService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            int pageSize = 5;
            int pageNo = 1;
            string searchString = string.Empty;

            var tasks = _employeeTaskService.GetTasksForList(pageSize, pageNo, searchString);

            var model = new ListEmployeeTaskForList
            {
                Tasks = tasks.Tasks,
                PageSize = pageSize,
                CurrentlyPage = pageNo,
                Count = tasks.Count
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNo, string searchString)
        {
            if (!pageNo.HasValue || pageNo <= 0)
            {
                pageNo = 1;
            }

            var tasks = _employeeTaskService.GetTasksForList(pageSize, pageNo.Value, searchString);

            var model = new ListEmployeeTaskForList
            {
                Tasks = tasks.Tasks,
                PageSize = pageSize,
                CurrentlyPage = pageNo.Value,
                Count = tasks.Count
            };

            return View(model);
        }


        // GET AddTask - Wyświetlanie formularza dodawania zadania
        [HttpGet]
        public IActionResult AddTask()
        {
            var employees = _employeeService.GetAllEmployees()
                .Select(e => new
                {
                    Id = e.Id,
                    FullName = e.FirstName + " " + e.LastName
                }).ToList();

            var employeeList = new SelectList(employees, "Id", "FullName");
            ViewBag.Employees = employeeList;

            var newTask = new NewEmployeeTaskVm();
            return View(newTask);
        }

        // POST AddTask - Dodawanie nowego zadania
        [HttpPost]
        public IActionResult AddTask(NewEmployeeTaskVm model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetEmployeeById(model.EmployeeId);
                if (employee != null)
                {
                    var task = new EmployeeTask
                    {
                        TaskName = model.TaskName,
                        TaskDate = model.TaskDate,
                        EmployeeId = model.EmployeeId,
                        IsCompleted = model.IsCompleted
                    };

                    _employeeTaskService.AddTask(task);

                    return RedirectToAction("Index", new { employeeId = model.EmployeeId });
                }
            }

            var employees = _employeeService.GetAllEmployees()
                .Select(e => new
                {
                    Id = e.Id,
                    FullName = e.FirstName + " " + e.LastName
                }).ToList();

            ViewBag.Employees = new SelectList(employees, "Id", "FullName");
            return View(model);
        }

        // GET EditTask - Wyświetlanie formularza edycji zadania
        [HttpGet]
        public IActionResult EditTask(int id)
        {
            var task = _employeeTaskService.GetTaskForEdit(id);
            if (task == null)
            {
                return NotFound();
            }

            // Pobranie listy pracowników
            var employees = _employeeService.GetAllEmployees() // Zakładam, że masz metodę, która zwraca wszystkich pracowników
                .Select(e => new
                {
                    Id = e.Id,
                    FullName = e.FirstName + " " + e.LastName
                }).ToList();

            ViewBag.Employees = new SelectList(employees, "Id", "FullName");

            var taskVm = new NewEmployeeTaskVm
            {
                Id = task.Id,
                TaskName = task.TaskName,
                TaskDate = task.TaskDate,
                IsCompleted = task.IsCompleted,
                EmployeeId = task.EmployeeId
            };

            return View(taskVm);
        }

        [HttpPost]
        public IActionResult EditTask(NewEmployeeTaskVm model)
        {
            if (ModelState.IsValid)
            {
                _employeeTaskService.UpdateTask(model);  // Zaktualizowanie zadania
                return RedirectToAction("Index");
            }

            // Jeśli walidacja się nie powiodła, pobierz ponownie listę pracowników
            var employees = _employeeService.GetAllEmployees()
                .Select(e => new
                {
                    Id = e.Id,
                    FullName = e.FirstName + " " + e.LastName
                }).ToList();

            ViewBag.Employees = new SelectList(employees, "Id", "FullName");

            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteTask(int id)
        {
            var task = _employeeTaskService.GetTaskForEdit(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Usuwa zadanie po potwierdzeniu
        [HttpPost, ActionName("DeleteTask")]
        [ValidateAntiForgeryToken]  // Dodaje ochronę przed atakami CSRF
        public IActionResult DeleteConfirmed(int id)
        {
            _employeeTaskService.DeleteTask(id);
            return RedirectToAction("Index");
        }
    }
}
