using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFDShop.Application.Interfaces;
using SFDShop.Application.ViewModel.WorkSchedule;
using SFDShop.Domain.Interface;
using SFDShop.Domain.Models;
using System;

namespace SFDShop.Controllers
{
    public class WorkScheduleController : Controller
    {
        private readonly IWorkScheduleService _workScheduleService;
        private readonly IEmployeeService _employeeService;

        public WorkScheduleController(IWorkScheduleService workScheduleService, IEmployeeService employeeService)
        {
            _workScheduleService = workScheduleService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Index(int pageNo = 1, int pageSize = 10, string searchString = "")
        {
            var workSchedules = _workScheduleService.GetAllWorkSchedulesForList(pageSize, pageNo, searchString);
            return View(workSchedules);
        }


        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNo, string searchString)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }

            if (string.IsNullOrEmpty(searchString))
            {
                searchString = string.Empty;
            }

            var workSchedules = _workScheduleService.GetAllWorkSchedulesForList(pageSize, pageNo.Value, searchString);

            return View(workSchedules);
        }

        [HttpGet]
        public IActionResult AddWorkSchedule()
        {
            var employees = _employeeService.GetAllEmployees();
            if (employees == null || !employees.Any())
            {
                // Jeżeli lista jest pusta
                ModelState.AddModelError("", "Brak dostępnych pracowników.");
            }

            var model = new NewWorkScheduleVm
            {
                Employees = employees.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.FullName
                })
            };

            return View("AddWorkSchedule", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddWorkSchedule(NewWorkScheduleVm model)
        {
            ModelState.Remove("Employees"); // Usuń walidację dla Employees

            // Walidacja modelu
            if (!ModelState.IsValid)
            {
                // Załaduj listę pracowników ponownie, jeśli model nie jest poprawny
                var employees = _employeeService.GetAllEmployees();
                model.Employees = employees.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.FullName
                }).ToList();

                return View(model);
            }

            // Zapisz wszystkie dni robocze
            foreach (var workDay in model.WorkDays)
            {
                var scheduleVm = new NewWorkScheduleVm
                {
                    EmployeeId = model.EmployeeId,
                    WorkDate = workDay.WorkDate != DateTime.MinValue ? workDay.WorkDate : DateTime.Now, // Użyj aktualnej daty, jeśli jest niewłaściwa
                    StartTime = workDay.StartTime,
                    EndTime = workDay.EndTime
                };

                // Przekaż model do metody serwisowej
                _workScheduleService.AddWorkSchedule(scheduleVm);
            }

            return RedirectToAction("Index");
        }





        // Wyświetlanie szczegółów grafiku
        [HttpGet]
        public IActionResult Details(int id)
        {
            var schedule = _workScheduleService.GetWorkScheduleById(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // Edytowanie istniejącego grafiku
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var schedule = _workScheduleService.GetWorkScheduleById(id);
            if (schedule == null)
            {
                return NotFound();
            }

            var model = new NewWorkScheduleVm
            {
                Id = schedule.Id,
                EmployeeId = schedule.EmployeeId,
                WorkDate = schedule.WorkDate,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                Employees = _employeeService.GetAllEmployees().Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.FullName
                }).ToList()
            };

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NewWorkScheduleVm model)
        {
            if (!ModelState.IsValid)
            {
                var employees = _employeeService.GetAllEmployees();
                model.Employees = employees.Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.FullName,
                    Selected = e.Id == model.EmployeeId
                });

                return View("EditWorkSchedule", model);
            }

            _workScheduleService.UpdateWorkSchedule(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            // Pobierz harmonogram do usunięcia
            var schedule = _workScheduleService.GetWorkScheduleById(id);
            if (schedule == null)
            {
                return NotFound();
            }

            // Mapa modelu na typ NewWorkScheduleVm
            var model = new NewWorkScheduleVm
            {
                Id = schedule.Id,
                EmployeeId = schedule.EmployeeId,
                WorkDate = schedule.WorkDate,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime
            };

            // Przekazanie poprawnego modelu do widoku Delete
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _workScheduleService.DeleteWorkSchedule(id);  // Usunięcie grafiku
            return RedirectToAction("Index");  // Powrót do listy
        }
    }
}
