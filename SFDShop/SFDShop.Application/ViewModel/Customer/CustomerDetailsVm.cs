using AutoMapper;
using SFDShop.Application.Mapping;
using SFDShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.Customer
{
    public class CustomerDetailsVm  : IMapFrom<SFDShop.Domain.Models.Customer>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public  ICollection<Domain.Models.Order> Orders { get; set; }

        public void Mapping (Profile profile) 
        { 
            profile.CreateMap<SFDShop.Domain.Models.Customer, CustomerDetailsVm>()
                .ForMember(s=>s.FullName, opt => opt.MapFrom(d=>d.FirstName + " " + d.LastName))
                .ForMember(s=>s.Orders,opt =>opt.Ignore());
        }
    }
}
