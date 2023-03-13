using EducationWebApi.DataAccess.Common.Enums;
using MediatR;

namespace EducationWebApi.DataAccess.Common;

public interface IMediatorPublisher
{
    Task Publish<TNotification>(TNotification notification);
    Task Publish<TNotification>(TNotification notification, PublishStrategy strategy);
    Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken);
    Task Publish<TNotification>(TNotification notification, PublishStrategy strategy, CancellationToken cancellationToken);
}
