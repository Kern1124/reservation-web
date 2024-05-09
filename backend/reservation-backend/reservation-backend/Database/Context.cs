using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using reservation_backend.Users;

namespace reservation_backend.Database;

public class Context : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public Context(){}

    public Context(DbContextOptions<Context> options) : base(options)
    {
        Database.EnsureCreated();
    }
}