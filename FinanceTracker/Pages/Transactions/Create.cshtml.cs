using FinanceTracker.Data;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceTracker.Pages.Transactions;

public class CreateModel : PageModel
{
    private readonly AppDbContext _db;

    public CreateModel(AppDbContext db)
    {
        _db = db;
    }

    [BindProperty] //binds automatically to inputs.
    public Transaction Transaction { get; set; } = new();

      public static readonly string[] Categories = // List of predefined categories for transactions.
     [
      "General",
      "Food",
      "Transport",
      "Bills",
      "Shopping",
      "Entertainment",
      "Health",
      "Salary",
      "Subscription",
      "Investment",
      "Other"
    ];
    public void OnGet()
    {
        //empty to show form
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) // valiation check if failled -> returns to main page.
        {
            return Page();
        }

        await _db.Transactions.AddAsync(Transaction);
        await _db.SaveChangesAsync();

        return RedirectToPage("/Transactions/Index"); // redirects to main page after saving transaction.
    }

   
}