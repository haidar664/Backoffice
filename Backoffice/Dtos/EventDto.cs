using Backoffice.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backoffice.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        [Range(0, 1000000000)]
        public int? CategoryId { get; set; }

        public LookUpModel? Category { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int? Cost { get; set; }

        public string? Status { get; set; }
    }
}
