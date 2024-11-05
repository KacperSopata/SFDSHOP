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
    public class ItemDetailsVm : IMapFrom<SFDShop.Domain.Models.Item>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price  { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public bool IsAvailable { get; set; }
        public int TypeId { get; set; }
        public SFDShop.Domain.Models.Type ItemType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SFDShop.Domain.Models.Item, ItemDetailsVm>();
        }
    }
}
