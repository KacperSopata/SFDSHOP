using AutoMapper;
using SFDShop.Application.Mapping;
using System;

namespace SFDShop.Application.ViewModel.WorkSchedule
{
    public class WorkDay : IMapFrom<SFDShop.Domain.Models.WorkSchedule>
    {
        public DateTime WorkDate { get; set; }  // Data pracy
        public TimeSpan StartTime { get; set; }  // Godzina rozpoczęcia pracy
        public TimeSpan EndTime { get; set; }    // Godzina zakończenia pracy

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SFDShop.Domain.Models.WorkSchedule, WorkDay>()
                .ForMember(d => d.WorkDate, opt => opt.MapFrom(s => s.WorkDate))
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartTime))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndTime));
        }
    }
}
