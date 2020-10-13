using Microsoft.EntityFrameworkCore;

namespace ShoppingApi.Domain
{
    public class ShoppingDataContext : DbContext
    {
        public ShoppingDataContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }

        public DbSet<CurbsideOrder> CurbsideOrders { get; set; }
    }
}
