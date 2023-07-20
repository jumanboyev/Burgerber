namespace Burgerber.Domain.Entities
{
    public class Auditable:BaseEntity
    {
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

    }
}
