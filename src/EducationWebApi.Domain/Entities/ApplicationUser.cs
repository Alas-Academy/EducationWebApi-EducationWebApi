using EducationWebApi.Core.Entities;
using EducationWebApi.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationWebApi.Domain.Entities;
public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
        SocialMedias = new HashSet<SocialMedia>();
    }
    public string NameSurname { get; set; } = null!;
    public override string? PhoneNumber { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime Birthday { get; set; }
    public Gender Gender { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
    public bool IsTempPassword { get; set; }
    public string? ProfilePhotoUrl { get; set; }
    public ICollection<SocialMedia>? SocialMedias { get; set; }

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
