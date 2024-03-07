using CQRS_MediatR_Demo.Data;
using MediatR;

namespace CQRS_MediatR_Demo.Commands
{
    public record AddProductCommand(Product Product) : IRequest<Product>;
}
