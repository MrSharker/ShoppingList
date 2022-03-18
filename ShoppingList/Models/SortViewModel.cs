namespace ShoppingList.Models
{
    public class SortViewModel
    {
        public SortState NameSort { get; private set; } // значение для сортировки по имени
        public SortState DateSort { get; private set; }    // значение для сортировки по возрасту
        public SortState CategorySort { get; private set; }   // значение для сортировки по компании
        public SortState Current { get; private set; }     // текущее значение сортировки
 
        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            CategorySort = sortOrder == SortState.СategoryAsc ? SortState.СategoryDesc : SortState.СategoryAsc;
            Current = sortOrder;
        }
    }
}
