namespace CoreApi.Entities.Domin
{
    public interface IEntity
    {

    }
    public abstract class BaseEntity<TKey> : IEntity where TKey:struct
    {
        public TKey ID { get; set; }
    }
    public class BaseEntity:BaseEntity<int>
    {

    }
}
