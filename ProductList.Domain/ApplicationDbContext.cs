using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace ProductList.Domain;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) { }
    public DbSet<Product> Products => Set<Product>();
}

