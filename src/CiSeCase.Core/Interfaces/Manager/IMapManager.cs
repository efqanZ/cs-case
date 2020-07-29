namespace CiSeCase.Core.Interfaces.Manager
{
    public interface IMapManager
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}