namespace DomainBlocks.Domains;


public interface IEntity<T> : IEntity
{
    public T Id { get; set; }
}
public interface IEntity
{
    public DateTime? CreateOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string? LastModifiedBy { get; set; }
}
