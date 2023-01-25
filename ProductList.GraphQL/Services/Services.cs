using Microsoft.EntityFrameworkCore;

using ProductList.Domain;

namespace ProductList.GraphQL.Services;

public class Services : IServices
{
    private readonly ApplicationDbContext Db;

    public Services(ApplicationDbContext context)
    {
        Db = context;
    }

    public async Task<Guid> CreateProductAsync(Product product)
    {
        await Db.Products.AddAsync(product);
        await Db.SaveChangesAsync();

        return product.Id;
    }

    public async Task<bool> DeleteAllProductsAsync()
    {
        var products = Db.Products;

        Db.Products.RemoveRange(products);
        await Db.SaveChangesAsync();

        return !Db.Products.Any();

    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var product = await Db.Products.FindAsync(id);

        if (product is not null)
        {
            Db.Products.Remove(product);
            await Db.SaveChangesAsync();
        }

        return true;
    }

    public async Task<IEnumerable<Product>?> GetAllProductsAsync()
        => await Db.Products.ToListAsync();


    public async Task<Product?> GetProductByIdAsync(Guid id)
        => await Db.Products.FindAsync(id);

    public async Task<Product?> GetProductByNameAsync(string name)
        => await Db.Products.FirstOrDefaultAsync(p => p.Name == name);

    public async Task<Product> UpdateProductAsync(Guid id, Product product)
    {
        if (product == null)
        {
            throw new ArgumentException("ProductInput not found.");
        }

        product.Name = product.Name;
        product.Price = product.Price;
        product.ImagePath = product.ImagePath;

        Db.Products.Update(product);
        await Db.SaveChangesAsync();

        return product;

    }
}