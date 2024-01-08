using System.ComponentModel.DataAnnotations;

namespace ShoroukAcademy.ShaTask.Presentation.ViewModels;

public class AddInvoiceViewModel
{
    [Required]
    public int InvoiceHeaderId { get; set; }
    [Required]
    public string ItemName { get; set; } = null!;
    [Required]
    public double ItemCount { get; set; }
    [Required]
    public double ItemPrice { get; set; }
}
