
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationWebApi.Domain.Common;

public class BaseEntity
{
    public virtual Guid Id { get; set; }

    #region Domain Events are used to publish events to the Mediator
    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    #endregion
}
