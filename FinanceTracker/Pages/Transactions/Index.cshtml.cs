using FinanceTracker.Data;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Pages.Transactions;

public class IndexModel : PageModel 
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
            .OrderByDescending(t => t.Date) // Orders Transactions by Date in descending order easier to read
            .ToListAsync();


            var today = DateTime.Today;
            var start = new DateTime(today.Year, today.Month, 1);
            var end = start.AddMonths(1);

            var monthTx = Transactions.Where(t => t.Date >= start && t.Date < end).ToList();

            MonthlyIncome = 0;
            MonthlyExpenses = 0;

            foreach (var trans in monthTx)
            {
                if (trans.Amount > 0)
                {
                    MonthlyIncome += trans.Amount;
                }
                else if (trans.Amount < 0)
                {
                    MonthlyExpenses += -trans.Amount;
                }
            } //easy way to calculate monthly expenses without getting complicated. 

    }

    public decimal MonthlyIncome { get; set; }
    public decimal MonthlyExpenses { get; set; }
    public decimal MonthtlyNet => MonthlyIncome - MonthlyExpenses;
}
// works