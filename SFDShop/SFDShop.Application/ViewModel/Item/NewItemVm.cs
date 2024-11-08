﻿using AutoMapper;
using FluentValidation;
using SFDShop.Application.Mapping;
using SFDShop.Application.ViewModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFDShop.Application.ViewModel.Item
{
    public class NewItemVm : IMapFrom<SFDShop.Domain.Models.Item>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public bool IsAvailable { get; set; }
        public SFDShop.Domain.Models.Type Type { get; set; }


        public void Mapping(Profile profile) // Mapper For Post actions (Add Action)
        {
            profile.CreateMap<SFDShop.Domain.Models.Item, NewItemVm>().ReverseMap();
               
        }

        public class NewItemValidator : AbstractValidator<NewItemVm>
        {
            public NewItemValidator()
            {
                RuleFor(x => x.Id).NotNull();
                RuleFor(x => x.Name).NotNull().MinimumLength(5);
                RuleFor(x => x.Description).NotNull().MinimumLength(20);
                RuleFor(x => x.TypeId).NotNull();
                RuleFor(x => x.Price).NotNull();
                RuleFor(x => x.Count).NotNull();
                RuleFor(x => x.IsAvailable).NotNull();
                RuleFor(x => x.Type).NotNull();
            }
        }

    }
}
