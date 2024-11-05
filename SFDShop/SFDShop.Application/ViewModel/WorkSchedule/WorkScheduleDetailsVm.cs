using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFDShop.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.WorkSchedule
{
    public class WorkScheduleDetailsVm : IMapFrom<SFDShop.Domain.Models.WorkSchedule>
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; } // Id pracownika przypisanego do grafiku
        public string EmployeeFullName { get; set; } // Imię i nazwisko pracownika
        public DateTime WorkDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public SelectList Employees { get; set; } // <--- DODAJ TO POLE

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SFDShop.Domain.Models.WorkSchedule, WorkScheduleDetailsVm>()
                .ForMember(d => d.EmployeeFullName, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
                .ForMember(d => d.WorkDate, opt => opt.MapFrom(s => s.WorkDate))
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartTime))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndTime))
                .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.EmployeeId));
        }
    }
}