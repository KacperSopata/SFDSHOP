using AutoMapper;
using SFDShop.Application.Mapping;
using SFDShop.Application.ViewModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.Order
{
    public class NewOrderVm : IMapFrom<SFDShop.Domain.Models.Order>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public string PhoneNumber { get; set; }
        public int ItemId { get; set; }
        public double Price { get; set; }
        public int CountOfItems { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SFDShop.Domain.Models.Order, NewOrderVm>().ReverseMap();
        }
    }
}
