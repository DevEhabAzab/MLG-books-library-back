using Microsoft.EntityFrameworkCore;
using MLG.Models;

namespace MLG
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }
        
        public DbSet<Book> Books { get; set; } 
       
    }
}
