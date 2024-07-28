using System.ComponentModel.DataAnnotations;

namespace Backoffice.Dtos
{
    public class EditUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(256, ErrorMessage = "Maximum length for email is {1}")]
        public string Email { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "Maximum length for role is {1}")]
        public string Role { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "Maximum length for full name is {1}")]
        [RegularExpression("^[A-Za-z]+$")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public char Gender { get; set; }

        public string Profession { get; set; }
        public string Nationality { get; set; }
        public string EmergencyPhoneNumber { get; set; }
    }
}
