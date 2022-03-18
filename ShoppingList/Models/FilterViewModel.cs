using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoppingList.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Category> categories, Category? category, string name)
        {
            Categories = new List<SelectListItem>();
            foreach (var categoryItem in categories)
            {
                Categories.Add(new SelectListItem() { 
                    Value = ((int)categoryItem).ToString(),
                    Text = categoryItem.GetDescription(),
                    Selected = categoryItem == category ? true : false 
                });
            };

            SelectedСategory = category == null ? Category.Nothing : category.Value;
            SelectedName = name;
        }
        public List<SelectListItem> Categories { get; private set; }
        public Category SelectedСategory { get; private set; } 
        public string SelectedName { get; private set; }
    }
}
