using Microsoft.EntityFrameworkCore;
using ShoppingList.Models;

namespace ShoppingList.App_Data
{
    public class ShoppingListContext : DbContext
    {
        public DbSet<ReceiptItem> ReceiptItems { get; set; }
        public ShoppingListContext(DbContextOptions<ShoppingListContext> options)
            : base(options)
        {
        }
    }
}
