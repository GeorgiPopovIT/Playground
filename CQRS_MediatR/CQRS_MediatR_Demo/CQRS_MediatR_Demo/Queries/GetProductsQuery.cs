using CQRS_MediatR_Demo.Data;
using MediatR;

namespace CQRS_MediatR_Demo.Queries;

public record GetProductsQuery() : IRequest<IEnumerable<Product>>;
