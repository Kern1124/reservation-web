using reservation_backend.Database;
using reservation_backend.Enums;
using reservation_backend.Models;
using reservation_backend.Users;

namespace reservation_backend.Interfaces;

public interface IUserService
{
    public (RegisterResult, User?) Register(User user, string password);
    public (LoginResult, User?) Login(string email, string password);
    public User? GetUserById(int id);
    public List<Reservation> GetUserReservations(int id);

}