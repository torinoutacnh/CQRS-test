namespace CQRS.Db.Models
{
    public abstract class Entity
    {
        public Guid Id { get; init; }

        protected Entity()
        {
            Id=Guid.NewGuid();
        }
    }
}
