using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using reservation_backend.Dto;
using reservation_backend.Models;

namespace reservation_backend.Database;

public class Context : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<TimeSlotSpan> TimeSlotSpans { get; set; }
    public virtual DbSet<Reservation> Reservations { get; set; }
    public virtual DbSet<OfferedService> OfferedServices { get; set; }
    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Notification> Notifications { get; set; }

    public Context(){}
    public Context(DbContextOptions<Context> options) : base(options)
    {
        Database.EnsureCreated();
        if (!Countries.Any())
        {
            using (var transaction = Database.BeginTransaction())
            {
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error,
                    Error = (sender, eventArgs) => {
                        Console.WriteLine(eventArgs.ErrorContext.Error.Message); 
                        eventArgs.ErrorContext.Handled = true;
                    }
                };
                var countries = JsonConvert
                    .DeserializeObject<CountriesDto>(File
                        .ReadAllText("Data/countries.json"), settings)!.Data.Select(c => new Country(c));
                Countries.AddRange(countries);
                transaction.Commit(); 
                SaveChanges();
            }
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OfferedService>().HasMany(i => i.TimeSlots).WithOne();
        modelBuilder.Entity<OfferedService>().HasOne(i => i.Location);
        modelBuilder.Entity<User>().HasMany(i => i.Notifications).WithOne(m => m.Recipient);
    }
    
}