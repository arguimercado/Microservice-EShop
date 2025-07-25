﻿namespace DomainBlocks.Domains;


public interface IAggregateRoot<T> : IAggregateRoot, IEntity<T>
{
}
public interface IAggregateRoot : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    IDomainEvent[] GetDomainEvents();

}
