using EducationWebApi.Application.Enums;
using MediatR;

namespace EducationWebApi.Application.Common;

public interface IMediatorPublisher
{
    Task Publish<TNotification>(TNotification notification);
    Task Publish<TNotification>(TNotification notification, PublishStrategy strategy);
    Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken);
    Task Publish<TNotification>(TNotification notification, PublishStrategy strategy, CancellationToken cancellationToken);
}
