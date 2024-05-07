using Microsoft.EntityFrameworkCore;
using RestClientExample.RestApi.Models;

namespace RestClientExample.RestApi;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BlogModel> Blogs { get; set; }
}
