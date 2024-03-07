using MediatR;
using Microsoft.AspNetCore.Identity.Data;

namespace CQRS_MediatR_Demo.Interfaces;

public interface ISender
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

    Task<object?> Send(object request, CancellationToken cancellationToken = default);
}
