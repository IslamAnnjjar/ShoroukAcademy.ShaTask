using System.ComponentModel.DataAnnotations;

namespace ShoroukAcademy.ShaTask.Presentation.ViewModels;

public class AddCashierViewModel
{
    [Required]
    public string? CashierName { get; set; }

    [Required]
    public int BranchId { get; set; }
}
