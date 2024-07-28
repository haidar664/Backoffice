using System.ComponentModel.DataAnnotations;
namespace Backoffice.Models

{
    public class LookUpModel
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; }
        public string? Name { get; set; }

        public int? Order { get; set; }
    }
}
