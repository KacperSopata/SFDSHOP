using AutoMapper;
using SFDShop.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.Employee
{
    public class EmployeeForListVm : IMapFrom<SFDShop.Domain.Models.Employee>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SFDShop.Domain.Models.Employee, EmployeeForListVm>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.Position, opt => opt.MapFrom(s => s.Position))
                .ReverseMap();
        }
    }
}