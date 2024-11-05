using AutoMapper;
using SFDShop.Application.Mapping;
using SFDShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.Employee
{
    public class EmployeeDetailsVm : IMapFrom<SFDShop.Domain.Models.Employee>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public ICollection<SFDShop.Domain.Models.EmployeeTask> Tasks { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SFDShop.Domain.Models.Employee, EmployeeDetailsVm>()
                 .ForMember(d => d.FullName, opt => opt.MapFrom(e => e.FirstName + " " + e.LastName))
                 .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(e => e.PhoneNumber)) // Mapowanie numeru telefonu
                  .ForMember(d => d.EmailAddress, opt => opt.MapFrom(e => e.EmailAddress))
                 .ForMember(d => d.Tasks, opt => opt.Ignore()); // Ignoruj mapowani
        }
    }
}
