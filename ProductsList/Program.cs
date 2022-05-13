using Microsoft.EntityFrameworkCore;
using ProductsList;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.MapGet("/Get/{id:Guid}", (Guid id, ApplicationDbContext db) => db.Products.Find(id));

app.MapPost("/Post", (ProductDTO newProduct, ApplicationDbContext db) =>
{
    db.Add(newProduct);
    db.SaveChanges();
});

app.MapGet("/Get/AllProducts", ( ApplicationDbContext db) => db.Products);


app.MapPut("/Put/Product", (ProductDTO product, ApplicationDbContext db) =>
{
    var DbProduct = db.Find<ProductDTO>(product.Id);

    DbProduct.Price = product.Price;
    DbProduct.ImagePath = product.ImagePath;
    DbProduct.Name = product.Name;

    db.SaveChanges();
});

app.MapDelete("/Delete/Product", (Guid id, ApplicationDbContext db) =>
{
    var product = db.Products.Find(id);

    if (product is null) return Results.Problem();

    db.Products.Remove(product);

    db.SaveChanges();

    return Results.Ok();
});

app.MapDelete("/Delete/AllProducts", (ApplicationDbContext db) =>
{
    var allproducts = db.Products;

    db.Products.RemoveRange(allproducts);

    db.SaveChanges();

});


app.Run();
