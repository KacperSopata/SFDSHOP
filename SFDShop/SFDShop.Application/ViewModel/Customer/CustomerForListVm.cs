using AutoMapper;
using SFDShop.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.Customer
{
    public class CustomerForListVm : IMapFrom<SFDShop.Domain.Models.Customer>
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SFDShop.Domain.Models.Customer, CustomerForListVm>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
                .ReverseMap(); 
        }

    }
}
