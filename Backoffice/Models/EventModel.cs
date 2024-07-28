using Backoffice.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backoffice.Models
{
    public class EventModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "Maximum length for name is {1}")]
        public string? Name { get; set; }

        [StringLength(256, ErrorMessage = "Maximum length for description is {1}")]
        public string? Description { get; set; }

        [Range(0, 1000000000)]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public LookUpModel? Category { get; set; }

        [Required]
        public DateTime? DateFrom { get; set; }

        [Required]
        public DateTime? DateTo { get; set; }

        [Required]
        [Range(0, 1000000000)]
        public int? Cost { get; set; }

        [StringLength(256, ErrorMessage = "Maximum length for status is {1}")]
        public string? Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool Deleted { get; set; } = false;

    }
}
