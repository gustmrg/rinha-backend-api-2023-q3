using Microsoft.EntityFrameworkCore;
using RinhaDeBackEnd.API.Models;

namespace RinhaDeBackEnd.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Person> Persons => Set<Person>();
}