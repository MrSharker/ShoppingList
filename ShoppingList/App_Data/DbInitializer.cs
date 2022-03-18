namespace ShoppingList.App_Data
{
    public class DbInitializer
    {
        public static void Initialize(ShoppingListContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}