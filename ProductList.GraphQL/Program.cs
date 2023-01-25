using Microsoft.EntityFrameworkCore;

using ProductList.Domain;
using ProductList.GraphQL.Mutation;
using ProductList.GraphQL.Query;
using ProductList.GraphQL.Resolvers;
using ProductList.GraphQL.Services;
using ProductList.GraphQL.Types;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddScoped<IServices, Services>();
builder.Services.AddScoped<Resolvers>();


builder.Services.AddGraphQL(sp => SchemaBuilder.New()
.AddServices(sp)
.AddType<ProductType>()
.AddQueryType<Query>()
.AddMutationType<Mutation>()
.AddResolver<Resolvers>()
.Create());


var app = builder.Build();

app.UseRouting()
    .UseEndpoints(endpoint => endpoint.MapGraphQL());

app.Run();
