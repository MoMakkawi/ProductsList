using System;
using System.Threading.Tasks;

using ProductList.Domain;

using ProductResolver = ProductList.GraphQL.Resolvers.Resolvers;

namespace ProductList.GraphQL.Mutation;

public class Mutation
{
    private readonly ProductResolver resolvers;

    public Mutation(ProductResolver resolvers)
    {
        this.resolvers = resolvers;
    }

    public async Task<Guid> CreateProductAsync(ProductInput productInput)
        => await resolvers.CreateProductAsync(productInput);

    public async Task<Product> UpdateProductAsync(Guid id, ProductInput productInput)
        => await resolvers.UpdateProductAsync(id, productInput);

    public async Task<bool> DeleteProductAsync(Guid id)
        => await resolvers.DeleteProductAsync(id);

    public async Task<bool> MyMethodAsync()
        => await resolvers.DeleteAllProductsAsync();
}