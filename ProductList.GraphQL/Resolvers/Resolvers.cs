using ProductList.Domain;
using ProductList.GraphQL.Services;
using ProductList.GraphQL.Types;

namespace ProductList.GraphQL.Resolvers;

public class Resolvers
{
    private readonly IServices services;

    public Resolvers(IServices services)
    {
        this.services = services;
    }

    #region Mutations

    public async Task<bool> DeleteAllProductsAsync()
    => await services.DeleteAllProductsAsync();

    public async Task<bool> DeleteProductAsync(Guid id)
    => await services.DeleteProductAsync(id);
    public Task<Guid> CreateProductAsync(ProductInput productInput)
    {
        var product = MappToProductFromProductInput(productInput);

        return services.CreateProductAsync(product);
    }
    public async Task<Product> UpdateProductAsync(Guid id, ProductInput productInput)
    {
        var product = MappToProductFromProductInput(productInput);
        return await services.UpdateProductAsync(id, product);
    }
    #endregion

    #region Queries

    public async Task<IQueryable<Product>?> GetAllProductsAsync()
    {
        try
        {
            return (await services.GetAllProductsAsync())!.AsQueryable();
        }
        catch (NullReferenceException)
        {
            return Enumerable.Empty<Product>().AsQueryable();
        }
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    => await services.GetProductByIdAsync(id);

    public async Task<Product?> GetProductByNameAsync(string name)
    => await services.GetProductByNameAsync(name);

    #endregion

    #region Helpers
    Func<ProductInput,Product> MappToProductFromProductInput = (pi) => new Product
    {
        Name = pi.Name,
        Price = pi.Price,
        ImagePath = pi.ImagePath,
    };
    #endregion

}