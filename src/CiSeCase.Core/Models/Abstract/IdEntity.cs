namespace CiSeCase.Core.Models.Abstract
{
    public class IdEntity<T> : Entity, IIdEntity<T>
    {
        public virtual T Id { get; set; }
    }
}