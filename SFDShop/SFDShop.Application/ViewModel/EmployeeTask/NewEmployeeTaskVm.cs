using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SFDShop.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.EmployeeTask
{
    public class NewEmployeeTaskVm : IMapFrom<SFDShop.Domain.Models.EmployeeTask>
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public DateTime TaskDate { get; set; }
        public bool IsCompleted { get; set; }
        public int EmployeeId { get; set; }
      

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewEmployeeTaskVm, SFDShop.Domain.Models.EmployeeTask>().ReverseMap();
            profile.CreateMap<SFDShop.Domain.Models.EmployeeTask, NewEmployeeTaskVm>().ReverseMap();
        }

        public class NewEmployeeTaskValidator : AbstractValidator<NewEmployeeTaskVm>
        {
            public NewEmployeeTaskValidator()
            {
                RuleFor(x => x.TaskName).NotEmpty().MaximumLength(100);
                RuleFor(x => x.TaskDate).NotNull();
         
            }
        }
    }
}
