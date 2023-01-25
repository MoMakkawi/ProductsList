using Google.Protobuf.WellKnownTypes;

using Grpc.AspNetCore.Server;

using Grpc.Core;
using ProductList.Domain;

namespace ProductList.gRPC.Services;

[BindServiceMethod(typeof(ProductService), "AddProduct")]
public class ProductService : ProductServices.ProductServicesBase
{
    public ApplicationDbContext Db { get; }
    public ProductService(ApplicationDbContext db)
    {
        Db = db;
    }
    
    public override async Task<ProductResponse> AddProduct(Product request, ServerCallContext context)
    {
        var Product = MappToDomainProduct(gRPCProduct: request);
        await Db.Products.AddAsync(Product);
        await Db.SaveChangesAsync();

        return new ProductResponse
        {
            Message = $"product added and has id = {Product.Id} at {DateTime.UtcNow}"
        };
    }


    public override async Task<ProductResponse> DeleteProduct(ProductRequest request, ServerCallContext context)
    {
        var Product = await Db.Products.FindAsync(request.Id);
        
        Db.Products.Remove(Product);

        return new ProductResponse
        {
            Message = $"product deleted had id = {request.Id} at {DateTime.UtcNow}"
        };
    }
    public async override Task<ProductResponse> DeleteAllProducts(Empty request, ServerCallContext context)
    {
        var allproducts = Db.Products;
        Db.Products.RemoveRange(allproducts);
        await Db.SaveChangesAsync();

        return new ProductResponse
        {
            Message = $"products deleted at {DateTime.UtcNow}"
        };
    }

    public async override Task GetAllProducts(Empty request, IServerStreamWriter<Product> responseStream, ServerCallContext context)
    {
        var products = Db.Products;

        foreach (var product in products)
        {
            var gRPCProduct = MappTogRPCProduct(product);
            await responseStream.WriteAsync(gRPCProduct);
        }
    }

    public override async Task<Product?> GetProduct(ProductRequest request, ServerCallContext context)
    {
        var DomainProduct = await Db.Products.FindAsync(request.Id);

        if (DomainProduct is null) return null;

        return MappTogRPCProduct(DomainProduct);
    }

    public override async Task<ProductResponse> UpdateProduct(Product request, ServerCallContext context)
    {
        var DbProduct = await Db.FindAsync<Product>(request.Id);
        if(DbProduct is null) return new ProductResponse { Message = $"Product by id : {request.Id} Not Found" };

        DbProduct.Price = request.Price;
        DbProduct.ImagePath = request.ImagePath;
        DbProduct.Name = request.Name;

        return new ProductResponse
        {
            Message = $"product has id = {DbProduct.Id} updated at {DateTime.UtcNow}"
        };

    }

    #region Helpers
    private static Domain.Product MappToDomainProduct(Product gRPCProduct)
         => new()
        {
            Name = gRPCProduct.Name,
            Price = gRPCProduct.Price,
            ImagePath = gRPCProduct.ImagePath
        };
    private static Product MappTogRPCProduct(Domain.Product DominProduct)
     => new()
     {
         Id = DominProduct.Id.ToString(),
         Name = DominProduct.Name,
         Price = DominProduct.Price,
         ImagePath = DominProduct.ImagePath
     };

    #endregion
}
