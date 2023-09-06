using System.ComponentModel.DataAnnotations;

namespace LegalGen_Backend.Models
{
    public class ResearchBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }

        public string UserId { get; set; }
        //public User User { get; set; }

        public ICollection<LegalInformation> LegalInformation { get; set; }
    }

}
