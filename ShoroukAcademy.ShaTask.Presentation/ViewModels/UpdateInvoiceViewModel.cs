using System.ComponentModel.DataAnnotations;

namespace ShoroukAcademy.ShaTask.Presentation.ViewModels;
    public class UpdateInvoiceViewModel
{
    [Required]
    public long Id { get; set; }
    [Required]
    public int InvoiceHeaderId { get; set; }
    [Required]
    public string ItemName { get; set; } = null!;
    [Required]
    public double ItemCount { get; set; }
    [Required]
    public double ItemPrice { get; set; }
}
