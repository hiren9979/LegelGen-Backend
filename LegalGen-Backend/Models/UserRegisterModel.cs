using System.ComponentModel.DataAnnotations;

namespace Task_Management_System.Models
{
    public class UserRegisterModel
    {
        [Key]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Organization { get; set; }

        [Required]
        public string ContactDetails { get; set; }
    }
}
