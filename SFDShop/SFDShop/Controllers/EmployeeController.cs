using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFDShop.Application.Interfaces;
using SFDShop.Application.ViewModel.Employee;
using SFDShop.Domain.Interface;

namespace SFDShop.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Index(int pageSize = 5, int pageNo = 1, string searchString = "")
        {
            var model = _employeeService.GetAllEmployeesForList(pageSize, pageNo, searchString);

            return View(model);
        }
        [HttpGet]
        public IActionResult EmployeeDetails(int id)
        {
            var employeeDetails = _employeeService.GetEmployeeDetails(id);
            if (employeeDetails == null)
            {
                return NotFound();
            }
            return View(employeeDetails);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View(new NewEmployeeVm());
        }

        [HttpPost]
        public IActionResult AddEmployee(NewEmployeeVm model)
        {
            if (ModelState.IsValid)
            {
                var newEmployeeId = _employeeService.AddEmployee(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            var employee = _employeeService.GetEmployeeForEdit(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult EditEmployee(NewEmployeeVm model)
        {
            if (ModelState.IsValid)
            {
                _employeeService.UpdateEmployee(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            _employeeService.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}
