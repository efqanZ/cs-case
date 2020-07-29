using AutoMapper;
using CiSeCase.Core.Dtos;
using CiSeCase.Core.Models;

namespace CiSeCase.Infrastructure.Managers.Map.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketDto, Basket>();
            CreateMap<Basket, BasketDto>();
        }
    }
}