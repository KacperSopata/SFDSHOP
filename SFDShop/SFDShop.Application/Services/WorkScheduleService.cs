using AutoMapper;
using SFDShop.Application.Interfaces;
using SFDShop.Application.ViewModel.WorkSchedule;
using SFDShop.Domain.Interface;
using SFDShop.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SFDShop.Application.Services
{
    public class WorkScheduleService : IWorkScheduleService
    {
        private readonly IWorkScheduleRepository _workScheduleRepository;
        private readonly IMapper _mapper;

        public WorkScheduleService(IWorkScheduleRepository workScheduleRepository, IMapper mapper)
        {
            _workScheduleRepository = workScheduleRepository;
            _mapper = mapper;
        }
        public void AddWorkSchedule(NewWorkScheduleVm scheduleVm)
        {
            var schedule = _mapper.Map<WorkSchedule>(scheduleVm);
            _workScheduleRepository.Add(schedule);
        }
        public void UpdateWorkSchedule(NewWorkScheduleVm model)
        {
            var schedule = _mapper.Map<WorkSchedule>(model);
            _workScheduleRepository.Update(schedule);
        }

        public void DeleteWorkSchedule(int id)
        {
            _workScheduleRepository.Delete(id);
        }

        public WorkScheduleDetailsVm GetWorkScheduleById(int id)
        {
            var schedule = _workScheduleRepository.GetById(id);
            return schedule != null ? _mapper.Map<WorkScheduleDetailsVm>(schedule) : null;
        }

        public ListWorkScheduleForList GetAllWorkSchedulesForList(int pageSize, int pageNo, string searchString)
        {
            // Logika do pobierania harmonogramów z repozytorium
            var schedules = _workScheduleRepository.GetAll() // Pobierz wszystkie harmonogramy
                .Where(ws => string.IsNullOrEmpty(searchString) || ws.Employee.FullName.Contains(searchString))
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new ListWorkScheduleForList
            {
                WorkSchedules = schedules.Select(ws => new WorkScheduleForListVm
                {
                    Id = ws.Id,
                    EmployeeFullName = ws.Employee.FullName,
                    WorkDate = ws.WorkDate,
                    StartTime = ws.StartTime,
                    EndTime = ws.EndTime
                }).ToList(),
                Count = _workScheduleRepository.GetCount(), // Możesz też mieć metodę do policzenia
                PageSize = pageSize,
                CurrentlyPage = pageNo
            };
        }


        public ListWorkScheduleForList GetWorkSchedulesByEmployeeId(int employeeId, int pageSize, int pageNo)
        {
            var schedules = _workScheduleRepository.GetByEmployeeId(employeeId)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var scheduleList = new ListWorkScheduleForList
            {
                WorkSchedules = schedules.Select(s => _mapper.Map<WorkScheduleForListVm>(s)).ToList(),
                CurrentlyPage = pageNo,
                PageSize = pageSize,
                Count = _workScheduleRepository.GetByEmployeeId(employeeId).Count()
            };
            return scheduleList;
        }
        public List<EmployeeWorkScheduleViewModel> GetWorkSchedulesGroupedByEmployee(string searchString)
        {
            var schedules = _workScheduleRepository.GetAll()
                .Where(ws => string.IsNullOrEmpty(searchString) ||
                             ws.Employee.FullName.Contains(searchString))
                .ToList();

            var groupedSchedules = schedules
                .GroupBy(s => s.EmployeeId) // Grupowanie według EmployeeId
                .Select(g => new EmployeeWorkScheduleViewModel
                {
                    EmployeeFullName = g.First().Employee.FullName,
                    WorkDays = g.Select(s => new WorkDay
                    {
                        WorkDate = s.WorkDate,
                        StartTime = s.StartTime,
                        EndTime = s.EndTime
                    }).ToList()
                }).ToList();

            return groupedSchedules;
        }
    }
}
