using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models;

public class Transaction
{
    public int Id { get; set; }

    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.Today;
// description is required and limted to 200 characters.
    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;
// default set to general, limtied to 50 charaters.
    [StringLength(50)]
    public string Category { get; set; } = "General";
// both postivie and negitve values can be input. 
    public decimal Amount { get; set; }
}