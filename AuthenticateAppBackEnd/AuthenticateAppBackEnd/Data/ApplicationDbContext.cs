using AuthenticateAppBackEnd.Data;
using AuthenticateAppBackEnd.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticateAppBackEnd.Data
{
    public class ApplicationDbContext : DbContext
    {
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
     public DbSet<User> Users { get; set; }
    }


}
