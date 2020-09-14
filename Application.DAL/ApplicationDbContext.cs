using Application.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Person> Persons { get; set; }
    }
}