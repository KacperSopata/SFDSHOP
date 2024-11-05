using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFDShop.Application.Mapping;
using SFDShop.Application.ViewModel.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SFDShop.Application.ViewModel.WorkSchedule
{
    public class NewWorkScheduleVm : IMapFrom<SFDShop.Domain.Models.WorkSchedule>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Wybierz pracownika.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Podaj datę pracy.")]
        public DateTime WorkDate { get; set; }

        [Required(ErrorMessage = "Podaj godzinę rozpoczęcia pracy.")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "Podaj godzinę zakończenia pracy.")]
        public TimeSpan EndTime { get; set; }

        [BindNever]
        public IEnumerable<SelectListItem> Employees { get; set; }
        public List<WorkDay> WorkDays { get; set; } = new List<WorkDay>();

        public void Mapping(Profile profile)
        {
            // Usunięto duplikaty mapowania
            profile.CreateMap<NewWorkScheduleVm, SFDShop.Domain.Models.WorkSchedule>().ReverseMap();
        }

        public class NewWorkScheduleValidator : AbstractValidator<NewWorkScheduleVm>
        {
            public NewWorkScheduleValidator()
            {
                RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("Wybierz pracownika.");
                RuleFor(x => x.WorkDate).NotEmpty().WithMessage("Podaj datę pracy.");
                RuleFor(x => x.StartTime).NotEmpty().WithMessage("Podaj godzinę rozpoczęcia pracy.");
                RuleFor(x => x.EndTime).NotEmpty().WithMessage("Podaj godzinę zakończenia pracy.");
                RuleFor(x => x).Must(HaveValidTimes).WithMessage("Godzina zakończenia musi być późniejsza niż godzina rozpoczęcia.");
            }

            private bool HaveValidTimes(NewWorkScheduleVm model)
            {
                return model.EndTime > model.StartTime;
            }
        }
    }
}
