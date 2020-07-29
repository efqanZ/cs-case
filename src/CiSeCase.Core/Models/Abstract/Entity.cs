namespace CiSeCase.Core.Models.Abstract
{
    public class Entity : EntityBase, IEntity
    {
        public bool Deleted { get; set; }
    }
}