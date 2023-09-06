using LegalGen_Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task_Management_System.Models;


namespace LegalGen_Backend.DBContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

        }
        public DbSet<ResearchBook> ResearchBooks { get; set; }
        public DbSet<LegalInformation> LegalInformations { get; set; }
        public DbSet<SearchQuery> SearchQueries { get; set; }

    }
}
