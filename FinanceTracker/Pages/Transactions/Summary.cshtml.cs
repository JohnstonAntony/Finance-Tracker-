using FinanceTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Pages.Transactions;

public class SummaryModel : PageModel
{
    private readonly AppDbContext _db;

    public SummaryModel(AppDbContext db)
    {
        _db = db;
    }

    [BindProperty(SupportsGet = true)]
    public int Year { get; set; } = DateTime.Today.Year; // GET request parameter for year defaulting to current year if left empty

    [BindProperty(SupportsGet = true)]
    public int Month { get; set; } = DateTime.Today.Month; // GET request parameter for month defaulting to current month if left empty

    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
    public decimal Net => Income - Expenses; // Calculated property for net amount

    public async Task OnGetAsync() // GET requests handler Income and Expenses set to 0 for every query
    {
        var startDate = new DateTime(Year, Month, 1);
        var endDate = startDate.AddMonths(1); 

        var transactions = await _db.Transactions
            .Where(t => t.Date >= startDate && t.Date < endDate)
            .ToListAsync();

        Income = 0;
        Expenses = 0;

        foreach (var trans in transactions) 
        {
            if (trans.Amount > 0)
            {
                Income += trans.Amount;
            }
            else
            {
                Expenses += -trans.Amount;
            }
        }
    }
}