using AutoMapper;
using SFDShop.Application.Mapping;
using SFDShop.Application.ViewModel.Customer;

namespace SFDShop.Application.ViewModel.Item
{
    public class ItemForListVm : IMapFrom<SFDShop.Domain.Models.Item>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public bool IsAvailable { get; set; }
        public SFDShop.Domain.Models.Type Type { get; set; }
        public string NamePrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SFDShop.Domain.Models.Item, ItemForListVm>();
        }
    }
}
