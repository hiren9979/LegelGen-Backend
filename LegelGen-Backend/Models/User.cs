using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LegalGen_Backend.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }

        public string ContactDetails { get; set; }

    }
}
