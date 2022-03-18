namespace ShoppingList.Models
{
    public class FilterPageSortModelcs
    {
        public IEnumerable<ReceiptItem> ReceiptItems { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
