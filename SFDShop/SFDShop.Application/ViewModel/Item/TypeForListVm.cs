using AutoMapper;
using SFDShop.Application.Mapping;
using SFDShop.Application.ViewModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.Item
{
    public class TypeForListVm  : IMapFrom<SFDShop.Domain.Models.Type>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SFDShop.Domain.Models.Type, TypeForListVm>();
        }
    }
}
