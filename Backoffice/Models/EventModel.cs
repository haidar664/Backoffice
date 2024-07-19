using Backoffice.Data;

namespace Backoffice.Models
{
    public class EventModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public LookUpModel Category { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int Cost { get; set; }
        public string? Status { get; set; }
        public List<AppUser>? RelatedGuides { get; set; }
        public List<AppUser>? RelatedMembers { get; set; }

    }
}
