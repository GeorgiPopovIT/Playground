﻿namespace Bookify.Domain.Abstractions;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected Entity(Guid id)
    {
        this.Id = id;
    }

    protected Entity()
    {

    }
    public Guid Id { get; init; }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
        => _domainEvents.ToList();

    public void ClearDomainEvents()
        => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);
}
