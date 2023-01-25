using Microsoft.EntityFrameworkCore;

using ProductList.Domain;
using ProductList.gRPC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();

app.MapGrpcService<ProductService>();

app.MapGet("/", () => "Hi from gRPC you can use this api for CRUD operation on Product");

app.Run();
