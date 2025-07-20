namespace BuildingBlocks.Commons.Aggregates;

public class AggregateRoot<T>
    where T : class
{
    public T Id { get; set; }
    public AggregateRoot(T id)
    {
        Id = id;
    }
}
