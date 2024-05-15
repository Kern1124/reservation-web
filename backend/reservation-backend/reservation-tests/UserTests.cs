/*
 * Classification of tested scenarios:
 *   - User not found
 *   - User already exists
 *   - Invalid mail
 *   - Invalid credentials
 *   - Empty credentials
 *
 * Classification of possible errors:
 *   - Integration error - some dependency doesn't work properly
 *   - Unit error - one of the used components doesn't work properly
 *   - System error - unexpected error due to architecture/os design
 *   - Parallel error - error due to parallel running of the components/program
 *   - Versioning error - error due to wrong versioning - wrong git rebase/merge etc.
 *   - Performance error - error due to performance - mostly timeout
 *   - General code error - is a subset of almost all previous errors - ends in an exception
 *   - General bug - running code that doesn't work according to specification
 */

using Microsoft.EntityFrameworkCore;
using Moq;
using reservation_backend.Database;
using reservation_backend.Enums;
using reservation_backend.Models;
using reservation_backend.Services;
using reservation_backend.Users;


namespace reservation_tests;

public class UserTests
{
    private static UserService InitializeMockedService()
    {
        // InstantiateTestUser chooses random salt and hashes password "test"
        // We test User Service and expect Hash Service to hash data properly
        // (HashingService is a minor dependency)
        var data = new List<User>
        {
            User.InstantiateTestUser("dave", "dave@gmail.com", "test"),
            User.InstantiateTestUser("waaa", "wwa@gmail.com", "test"),
            User.InstantiateTestUser("dummyExists", "dumdum@gmail.com", "test")
        }.AsQueryable();
            
            
            
        var mockSetUsers = new Mock<DbSet<User>>();
        mockSetUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSetUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);

        var mockContext = new Mock<Context>();
        mockContext.Setup(c => c.Users).Returns(mockSetUsers.Object);
            
        return new UserService(mockContext.Object);
    }
    
    public class UserLoginTests
    {
        
        [Fact]
        public void Login_UserNotFound_ReturnTupleWithNull()
        {
            var userService = InitializeMockedService();
            var mail = "idonotexist@example.com";
            var password = "password";

            var (resultEnum, user) = userService.Login(mail, password);
            Assert.Equal(LoginResult.UserNotFound, resultEnum);
            Assert.Null(user);
        }
        [Fact]
        public void Login_IncorrectPassword_ReturnTupleWithNull()
        {
            var userService = InitializeMockedService();

            var mail = "dumdum@gmail.com";
            var password = "wrongpassword";

            var (resultEnum, user) = userService.Login(mail, password);
            Assert.Equal(LoginResult.PasswordIncorrect, resultEnum);
            Assert.Null(user);
        }
        [Fact]
        public void Login_Success_ReturnTupleWithUser()
        {
            var userService = InitializeMockedService();

            var mail = "dumdum@gmail.com";
            var password = "test";

            var (resultEnum, user) = userService.Login(mail, password);
            Assert.NotNull(user);
            Assert.Equal(LoginResult.Success, resultEnum);
            Assert.Equal("dummyExists", user.Username);
            Assert.Equal("dumdum@gmail.com", user.MailAddress);
        }
    }
    public class UserRegisterTests
    {
        [Fact]
        public void Register_UserMailAlreadyExists_ReturnFailed()
        {
            var userService = InitializeMockedService();
            var testUser = new User("dave", "dave@gmail.com");
            var (resultEnum, user) = userService.Register(testUser, "test");
            Assert.Equal(RegisterResult.UserMailAlreadyExists, resultEnum);
            Assert.Null(user);
        }
        [Fact]
        public void Register_Empty_ReturnEmptyFailed()
        {
            var userService = InitializeMockedService();
            var testUser = new User("", "");
            var (resultEnum, user) = userService.Register(testUser, "");
            Assert.Equal(RegisterResult.Empty, resultEnum);
            Assert.Null(user);
        }
        [Fact]
        public void Register_InvalidMail_ReturnInvalidMailFailed()
        {
            var userService = InitializeMockedService();
            var testUser = new User("davey", "daveybrekekeNOTmAIL.com");
            var (resultEnum, user) = userService.Register(testUser, "sasa");
            Assert.Equal(RegisterResult.InvalidMail, resultEnum);
            Assert.Null(user);
        }
        [Fact]
        public void Register_Success_ReturnSuccess()
        {
            var userService = InitializeMockedService();
            var testUser = new User("davey", "davey@gmail.com");
            var (resultEnum, user) = userService.Register(testUser, "sasa");
            Assert.NotNull(user);
            Assert.Equal(RegisterResult.Success, resultEnum);
            Assert.Equal(user.MailAddress, testUser.MailAddress);
            Assert.Equal(user.Username, testUser.Username);
        }
    }
}