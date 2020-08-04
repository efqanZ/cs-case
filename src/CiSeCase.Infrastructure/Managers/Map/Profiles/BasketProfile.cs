using AutoMapper;
using CiSeCase.Core.Dtos;
using CiSeCase.Core.Models;

namespace CiSeCase.Infrastructure.Managers.Map.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketDto, Basket>().ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.AddDate));
            CreateMap<Basket, BasketDto>().ForMember(dest => dest.AddDate, opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}