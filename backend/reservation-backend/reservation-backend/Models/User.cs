using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using reservation_backend.Services;

namespace reservation_backend.Models;
[Table("Users")]
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string MailAddress { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public List<Reservation> Reservations { get; set; }
    public List<OfferedService> OwnedServices { get; set; }
    public User(string username, string mailAddress)
    {   
        Username = username;
        MailAddress = mailAddress;
    }
    public User(int id, string username, string mailAddress)
    {
        Id = id;
        Username = username;
        MailAddress = mailAddress;
    }

    public static User InstantiateTestUser(string username, string mailAddress, string password)
    {
        var user = new User(username, mailAddress);

        var (hash, salt) = HashService.HashInput(password);
        user.PasswordHash = hash;
        user.PasswordSalt = salt;
        return user;
    }
}