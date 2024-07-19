using Microsoft.AspNetCore.Identity;
using Backoffice.Models;
using System.ComponentModel.DataAnnotations;

namespace Backoffice.Data
{
    public class AppUser: IdentityUser
    {
        [StringLength(50, ErrorMessage = "Maximum length for Name is {1}")]
        public string? FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }
            
        public char? Gender { get; set; }
        public string? Profession { get; set; }
        public string? Nationality { get; set; }
        public string? EmergencyPhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool Deleted { get; set; } = false;

        public List<EventModel>? EventsResponsibleOf { get; set; }

        public List<EventModel>? EventsJoined { get; set; }

    }
}
