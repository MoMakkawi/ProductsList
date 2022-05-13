using Microsoft.EntityFrameworkCore;

namespace ProductsList;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) { }
    public DbSet<ProductDTO> Products => Set<ProductDTO>();
}
