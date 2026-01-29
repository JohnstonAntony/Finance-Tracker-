using FinanceTracker.Data;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Pages.Transactions;

public class IndexModel : PageModel // Razor page model for displaying transactions on request.
{
    private readonly AppDbContext _db;

    public IndexModel(AppDbContext db)
    {
        _db = db;
    }

    public List<Transaction> Transactions { get; set; } = new(); 

    public async Task OnGetAsync()
    {
        Transactions = await _db.Transactions
            .OrderByDescending(t => t.Date) // Orders Transactions by Date in descending order.
            .ToListAsync();
    }
}
// works