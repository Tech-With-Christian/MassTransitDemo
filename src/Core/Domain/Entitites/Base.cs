namespace Domain.Entitites
{
    public class Base
    {
        public Base()
        {
            Id = Guid.NewGuid();
            UpdatedAt = DateTime.Now;
        }

        public Base(Guid id, DateTime createdAt)
        {
            Id = id;
            UpdatedAt = createdAt;
        }

        public Guid Id { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}
