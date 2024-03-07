using CQRS_MediatR_Demo.Data;
using MediatR;

namespace CQRS_MediatR_Demo.Notifications;

public record ProductAddedNotification(Product Product) : INotification;
