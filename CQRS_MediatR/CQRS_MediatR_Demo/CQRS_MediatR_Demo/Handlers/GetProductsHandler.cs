using CQRS_MediatR_Demo.Data;
using CQRS_MediatR_Demo.Queries;
using MediatR;

namespace CQRS_MediatR_Demo.Handlers;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly FakeDataStore _fakeDataStore;

    public GetProductsHandler(FakeDataStore fakeDataStore)
    {
        _fakeDataStore = new FakeDataStore();
    }
    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        => await _fakeDataStore.GetAllProducts();
}
