using AutoMapper;
using SFDShop.Application.Interfaces;
using SFDShop.Application.ViewModel.EmployeeTask;
using SFDShop.Domain.Interface;
using SFDShop.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;  
using SFDShop.Application.ViewModel.EmployeeTask;
using Microsoft.EntityFrameworkCore;

public class EmployeeTaskService : IEmployeeTaskService
{
    private readonly IEmployeeTaskRepository _employeeTaskRepository;
    private readonly IMapper _mapper;

    public EmployeeTaskService(IEmployeeTaskRepository employeeTaskRepository, IMapper mapper)
    {
        _employeeTaskRepository = employeeTaskRepository;
        _mapper = mapper;
    }

    public ListEmployeeTaskForList GetTasksForList(int pageSize, int pageNo, string searchString)
    {
        var tasks = _employeeTaskRepository
      .GetAll()
      .Include(t => t.Employee) // Dołącz dane o pracowniku
      .Where(t => t.TaskName.Contains(searchString)
                  || t.Employee.FirstName.Contains(searchString)
                  || t.Employee.LastName.Contains(searchString))
      .ToList();


        // Mapa zadania na EmployeeTaskForListVm, w tym mapowanie pełnego imienia i nazwiska pracownika
        var employeeTasksForList = _mapper.Map<List<EmployeeTaskForListVm>>(tasks);

        // Paginacja
        var tasksToShow = employeeTasksForList.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();

        // Tworzenie modelu do widoku
        var taskList = new ListEmployeeTaskForList
        {
            PageSize = pageSize,
            CurrentlyPage = pageNo,
            SearchString = searchString,
            Tasks = tasksToShow,
            Count = tasks.Count
        };

        return taskList;
    }

    public EmployeeTaskDetailsVm GetTaskDetails(int taskId)
    {
        var task = _employeeTaskRepository.GetById(taskId);
        return task != null ? _mapper.Map<EmployeeTaskDetailsVm>(task) : null;
    }

    public NewEmployeeTaskVm GetTaskForEdit(int taskId)
    {
        var task = _employeeTaskRepository.GetById(taskId);
        return task != null ? _mapper.Map<NewEmployeeTaskVm>(task) : null;
    }

    public void AddTask(EmployeeTask model)
    {
        // Logowanie, aby upewnić się, że dane są przekazywane do serwisu
        Console.WriteLine($"Model TaskName: {model.TaskName}, TaskDate: {model.TaskDate}, EmployeeId: {model.EmployeeId}");

        // Zapisanie w repozytorium
        _employeeTaskRepository.AddTask(model);

        // Logowanie sukcesu
        Console.WriteLine("Task added successfully to repository.");
    }
    public void UpdateTask(NewEmployeeTaskVm model)
    {
        var task = _mapper.Map<EmployeeTask>(model);
        _employeeTaskRepository.Update(task);
    }

    public int DeleteTask(int taskId)
    {
        var task = _employeeTaskRepository.GetById(taskId);
        if (task != null)
        {
            _employeeTaskRepository.Delete(taskId);
            return task.EmployeeId;  // Zwracamy EmployeeId, aby przekierować po usunięciu
        }
        return 0;
    }
}