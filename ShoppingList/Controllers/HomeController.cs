using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.App_Data;
using ShoppingList.Models;
using System.Diagnostics;
using System.Globalization;

namespace ShoppingList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShoppingListContext _db;

        public HomeController(ILogger<HomeController> logger, ShoppingListContext context)
        {
            _logger = logger;
            _db = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Products(Category? category, string name = null, int page = 1, SortState sortOrder = SortState.DateDesc)
        {
            var pageSize = 10;
            IQueryable<ReceiptItem> products = _db.ReceiptItems;
            if (category != null && category != 0)
                products = products.Where(x => x.Category == category.Value);
            if (!String.IsNullOrEmpty(name))
                products = products.Where(x => x.Name.Contains(name));

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case SortState.NameAsc:
                    products = products.OrderBy(s => s.Name);
                    break;
                case SortState.DateAsc:
                    products = products.OrderBy(s => s.BuyTime);
                    break;
                case SortState.СategoryAsc:
                    products = products.OrderBy(s => s.Category);
                    break;
                case SortState.СategoryDesc:
                    products = products.OrderByDescending(s => s.Category);
                    break;
                default:
                    products = products.OrderByDescending(s => s.BuyTime);
                    break;
            }

            var count = await products.CountAsync();
            var items = await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();


            FilterPageSortModelcs viewModel = new FilterPageSortModelcs
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(Enum.GetValues(typeof(Category)).OfType<Category>().ToList(), category == null ? Category.Nothing : (Category)category.Value, name),
                ReceiptItems = items
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UpsertReceiptItem receipt)
        {
            try
            {
                var newReceipt = new ReceiptItem()
                {
                    Name = receipt.Name,
                    Category = receipt.Category,
                    Count = receipt.Count,
                    Price = decimal.Parse(receipt.Price),
                    BuyTime = receipt.BuyTime,
                };
                _db.ReceiptItems.Add(newReceipt);
                await _db.SaveChangesAsync();
                return RedirectToAction("Products");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return BadRequest();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                ReceiptItem receiptItem = await _db.ReceiptItems.FirstOrDefaultAsync(receipt => receipt.Id == id);
                if (receiptItem != null)
                {
                    var model = new UpsertReceiptItem()
                    {
                        Id = receiptItem.Id,
                        Name = receiptItem.Name,
                        Category = receiptItem.Category,
                        Count = receiptItem.Count,
                        Price = receiptItem.Price.ToString(),
                        BuyTime = receiptItem.BuyTime,
                    };
                    return PartialView(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpsertReceiptItem receiptItem)
        {
            var receipt = await _db.ReceiptItems.FirstOrDefaultAsync(r => r.Id == receiptItem.Id);
            receipt.Name = receiptItem.Name;
            receipt.Category = receiptItem.Category;
            receipt.Count = receiptItem.Count;
            receipt.Price = decimal.Parse(receiptItem.Price);
            receipt.BuyTime = receiptItem.BuyTime;
            _db.ReceiptItems.Update(receipt);
            await _db.SaveChangesAsync();
            return RedirectToAction("Products");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                ReceiptItem receiptItem = await _db.ReceiptItems.FirstOrDefaultAsync(receipt => receipt.Id == id);
                if (receiptItem != null)
                {
                    _db.ReceiptItems.Remove(receiptItem);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Products");
                }
            }
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}