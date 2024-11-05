using AutoMapper;
using SFDShop.Application.Interfaces;
using SFDShop.Application.ViewModel.Employee;
using SFDShop.Domain.Interface;
using SFDShop.Domain.Models;
using System.Collections.Generic;
using System.Linq;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public ListEmployeeForList GetAllEmployeesForList(int pageSize, int pageNo, string searchString)
    {
        // Pobieramy wszystkich pracowników i wyszukujemy na podstawie imienia
        var employees = _employeeRepository.GetAll()
                            .Where(e => e.FirstName.Contains(searchString) || string.IsNullOrEmpty(searchString))
                            .Skip((pageNo - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

        //// Sprawdzamy, czy lista pracowników jest pusta
        //if (employees == null || !employees.Any())
        //{
        //    throw new Exception("Lista pracowników jest pusta.");
        //}

        // Tworzymy listę EmployeeForListVm
        var employeeList = new ListEmployeeForList
        {
            Employees = employees.Select(e => new EmployeeForListVm
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Position = e.Position
            }).ToList(),
            CurrentlyPage = pageNo,
            PageSize = pageSize,
            SearchString = searchString,
            Count = _employeeRepository.GetAll().Count()
        };

        return employeeList;
    }


public EmployeeDetailsVm GetEmployeeDetails(int id)
    {
        var employee = _employeeRepository.GetById(id);
        return employee != null ? _mapper.Map<EmployeeDetailsVm>(employee) : null;
    }

    public NewEmployeeVm GetEmployeeForEdit(int id)
    {
        var employee = _employeeRepository.GetById(id);
        return employee != null ? _mapper.Map<NewEmployeeVm>(employee) : null;
    }

    public int AddEmployee(NewEmployeeVm model)
    {
        var employee = _mapper.Map<Employee>(model);
        _employeeRepository.Add(employee);
        return employee.Id;
    }

    public void UpdateEmployee(NewEmployeeVm model)
    {
        var employee = _mapper.Map<Employee>(model);
        _employeeRepository.Update(employee);
    }

    public void DeleteEmployee(int id)
    {
        _employeeRepository.Delete(id);
    }
    public List<Employee> GetAllEmployees()
    {
        // Pobranie pracowników z repozytorium
        var employees = _employeeRepository.GetAll()?.ToList() ?? new List<Employee>();
        return employees;
    }
    public Employee GetEmployeeById(int employeeId)
    {
        return _employeeRepository.GetById(employeeId); 
    }
}