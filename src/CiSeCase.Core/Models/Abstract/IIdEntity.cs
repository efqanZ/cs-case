namespace CiSeCase.Core.Models.Abstract
{
    public interface IIdEntity<T> : IEntity
    {
        T Id { get; set; }
    }
}