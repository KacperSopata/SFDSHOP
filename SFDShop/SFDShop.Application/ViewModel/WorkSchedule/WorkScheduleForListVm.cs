using AutoMapper;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SFDShop.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.WorkSchedule
{
    public class WorkScheduleForListVm : IMapFrom<SFDShop.Domain.Models.WorkSchedule>
    {
        public int Id { get; set; }
        public string EmployeeFullName { get; set; } // Imię i nazwisko pracownika
        public DateTime WorkDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string TaskName {  get; set; }



        public void Mapping(Profile profile)
        {
            profile.CreateMap<SFDShop.Domain.Models.WorkSchedule, WorkScheduleForListVm>()
                .ForMember(d => d.EmployeeFullName, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
                .ForMember(d => d.WorkDate, opt => opt.MapFrom(s => s.WorkDate))
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartTime))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndTime))
                .ForMember(d => d.TaskName, opt => opt.MapFrom(s => s.EndTime));


        }
    }
}