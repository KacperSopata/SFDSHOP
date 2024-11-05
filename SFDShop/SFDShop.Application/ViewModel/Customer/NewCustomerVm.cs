using AutoMapper;
using FluentValidation;
using SFDShop.Application.Mapping;
using SFDShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.Customer
{
    public class NewCustomerVm : IMapFrom<SFDShop.Domain.Models.Customer>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public void Mapping(Profile profile) // Mapper For Post actions (Add Action)
        {
            profile.CreateMap<NewCustomerVm, SFDShop.Domain.Models.Customer>().ReverseMap();
        }

        public class NewCustomerValidator : AbstractValidator<NewCustomerVm>
        {
            public NewCustomerValidator()
            { 
                RuleFor(x => x.Id).NotNull();
                RuleFor(x => x.FirstName).MaximumLength(20);
                RuleFor(x => x.LastName).MaximumLength(20);
                RuleFor(x => x.EmailAddress).EmailAddress();
                RuleFor(x => x.PhoneNumber).Length(9);
            }

        }

    }
}
