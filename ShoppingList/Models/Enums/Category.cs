using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Models
{
    public enum Category
    {
        [Display(Name = "Выбрать категорию")]
        [Description("Выбрать категорию")]
        Nothing = 0,
        [Display(Name = "Мясные продукты")]
        [Description("Мясные продукты")]
        Meat = 1,
        [Display(Name = "Молочные продукты")]
        [Description("Молочные продукты")]
        Dairy,
        [Display(Name = "Консервированые продукты")]
        [Description("Консервированые продукты")]
        Canned,
        [Display(Name = "Крупы")]
        [Description("Крупы")]
        Grains,
        [Display(Name = "Другое")]
        [Description("Другое")]
        Other
    }
}
