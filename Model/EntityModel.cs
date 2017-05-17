using Microsoft.EntityFrameworkCore;
namespace WebApplication
{
    public class EntityModelContext : DbContext
    {
            public EntityModelContext(DbContextOptions<EntityModelContext> options) : base(options)
            {

            }

            public DbSet<TestTable> TestTables { get; set; }

    }
}