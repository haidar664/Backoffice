using Backoffice.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backoffice.Models
{
    public class EventsToGuides
    {
        [Required]
        [StringLength(450, ErrorMessage = "Maximum length for Guide Id is {1}")]
        public string? GuideId { get; set; }
        [Display(Name = "Public User")]

        [ForeignKey("GuideId")]
        public AppUser? Guide { get; set; }

        [Required]
        [Range(0, 1000000000)]
        public int? EventId { get; set; }
        [Display(Name = "Event")]

        [ForeignKey("EventId")]
        public EventModel? Event { get; set; }

    }
}
