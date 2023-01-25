using ProductList.Domain;

namespace ProductList.GraphQL.Services;

public interface IServices
{
    //Queries
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<Product?> GetProductByNameAsync(string name);
    Task<IEnumerable<Product>?> GetAllProductsAsync();
    
    //Mutations
    Task<Guid> CreateProductAsync(Product productInput);
    Task<Product> UpdateProductAsync(Guid id, Product productInput);
    Task<bool> DeleteProductAsync(Guid id);
    Task<bool> DeleteAllProductsAsync();
}