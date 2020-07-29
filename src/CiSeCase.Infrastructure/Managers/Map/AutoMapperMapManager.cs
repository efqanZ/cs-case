using AutoMapper;
using CiSeCase.Core.Interfaces.Manager;

namespace CiSeCase.Infrastructure.Managers.Map
{
    public class AutoMapperMapManager : IMapManager
    {
        private readonly IMapper _mapper;
        public AutoMapperMapManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }
    }
}