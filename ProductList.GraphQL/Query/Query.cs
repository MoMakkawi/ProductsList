using ProductList.Domain;

using ProductResolver =  ProductList.GraphQL.Resolvers.Resolvers;

namespace ProductList.GraphQL.Query;

public class Query
{
    private readonly ProductResolver resolvers;

    public Query(ProductResolver resolvers)
    {
        this.resolvers = resolvers;
    }

    public async Task<IQueryable<Product?>?> GetProductsAsync()
        => await resolvers.GetAllProductsAsync();
    public async Task<Product?> GetProductByIdAsync(Guid id)
        => await resolvers.GetProductByIdAsync(id);
    public async Task<Product?> GetProductByNameAsunc(string name)
        => await resolvers.GetProductByNameAsync(name);
}