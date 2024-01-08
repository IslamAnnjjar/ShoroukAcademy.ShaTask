using System.ComponentModel.DataAnnotations;

namespace ShoroukAcademy.ShaTask.Presentation.ViewModels;
    public class UpdateCasherViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? CashierName { get; set; }
        [Required]
        public int BranchId { get; set; }
    }
