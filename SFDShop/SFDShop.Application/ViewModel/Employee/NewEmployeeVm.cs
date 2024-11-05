using AutoMapper;
using FluentValidation;
using SFDShop.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.Employee
{
    public class NewEmployeeVm : IMapFrom<SFDShop.Domain.Models.Employee>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewEmployeeVm, SFDShop.Domain.Models.Employee>().ReverseMap();
        }

        public class NewEmployeeValidator : AbstractValidator<NewEmployeeVm>
        {
            public NewEmployeeValidator()
            {
                RuleFor(x => x.Id).NotNull();
                RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
                RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
                RuleFor(x => x.Position).NotEmpty().MaximumLength(50);
            }
        }
    }
}