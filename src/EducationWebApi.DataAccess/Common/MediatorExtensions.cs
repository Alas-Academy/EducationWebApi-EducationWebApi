using EducationWebApi.Application.Common;
using EducationWebApi.Application.Enums;
using EducationWebApi.Domain.Common;
using EducationWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationWebApi.DataAccess.Common;

#region  MediatorExtensions is a helper class that will be used to publish domain events
public static class MediatorExtensions
{

    public static async Task DispatchDomainEvents(this IMediatorPublisher mediator, DbContext context)
    {
        var BaseEntiies = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var UserEntiies = context.ChangeTracker
             .Entries<ApplicationUser>()
             .Where(e => e.Entity.DomainEvents.Any())
             .Select(e => e.Entity);

        var domainEvents = BaseEntiies
              .SelectMany(e => e.DomainEvents)
              .Union(UserEntiies.SelectMany(e => e.DomainEvents))
              .ToList();

        BaseEntiies.ToList().ForEach(e => e.ClearDomainEvents());
        UserEntiies.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent, PublishStrategy.ParallelNoWait, CancellationToken.None);
    }
}
#endregion
