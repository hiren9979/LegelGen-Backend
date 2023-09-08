using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace LegalGen_Backend.Models
{
    public class LegalInformation
    {
        public int? Id { get; set; }
        public string? CaseType { get; set; }
        public string? Section { get; set; }
        public string? CaseNumber { get; set; }
        public string? Citation { get; set; }
        public string? Act { get; set; }
        public string? Petitioner { get; set; }
        public string? Respondent { get; set; }
        public string? Description { get; set; }
        public string? Document { get; set; }
        public DateTime? DateAdded { get; set; }
        public int? ResearchBookId { get; set; }
        public ResearchBook? ResearchBook { get; set; }
    }
}