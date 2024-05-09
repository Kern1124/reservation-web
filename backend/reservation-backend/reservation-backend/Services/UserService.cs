// I have excluded model definitions to keep this file shorter
/*
 * Code Checklist:
 * 1. Make sure that there shouldn’t be any project warnings, treat warning as errors
   2. It will be much better if Code Analysis is performed on a project (with all Microsoft Rules enabled) and then remove the warnings.
   3. Use asynchronous programming using C# async await where application, as it tremendously improves the performance
   4. Use C# new language features, for example use nameof operator to get the property/method names instead of hard coding it
   5. All unused usings need to be removed. Code cleanup for unnecessary code is always a good practice.
   6. ‘null’ check needs to be performed wherever applicable to avoid the Null Reference Exception at runtime, use C# 6.0 new feature “Null-conditional operators” for this, one example as given below
   7. Follow best practices while writing code by following SOLID principle and software design patterns
   8. Write loosely coupled components, follow dependency injection concept, extremely important, helps while doing unit testing as well
   9. Code Reusability: Extract a method if the same piece of code is being used more than once or you expect it to be used in future. Make some generic methods for repetitive task and put them in a related class so that other developers start using them once you intimate them. Develop user controls for common functionality so that they can be reused across the project.
   10. Code Consistency: Let’s say that an Int32 type is coded as int and String type is coded as string, then they should be coded in that same fashion across the application. But not like sometimes int and sometimes as Int32.
   11. Code Readability: Should be maintained so that other developers understand your code easily.
   12. Disposing of Unmanaged Resources like File I/O, Network resources, etc. They have to be disposed of once their usage is completed. Use usings block for unmanaged code, if you want to automatically handle the disposing of objects once they are out of scope.
   13. Proper implementation of Exception Handling (try/catch and finally blocks) and logging of exceptions.
   14. Naming conventions to be followed always. Generally for variables/parameters, follow Camel casing and for method names and class names, follow Pascal casing.
   15. Making sure that methods have less number of lines of code. Not more than 20 to 30 lines
   16. Timely check-in/check-out of files/pages at source control (like TFS).
   17. Peer code reviews. Swap your code files/pages with your colleagues to perform internal code reviews.
   18. Unit Testing. Write developer test cases and perform unit testing to make sure that basic level of testing is done before it goes to QA testing.
   19. Avoid nested for/foreach loops and nested if conditions as much as possible.
   20. Use anonymous types if code is going to be used only once.
   21. Try using LINQ queries and Lambda expressions to improve Readability.
   22. Use PLINQ wherever applicable, as it makes parallel operation within LINQ query and improves the performance
   23. Proper usage of var, object, and dynamic keywords. They have some similarities due to which most of the developers are confused or don’t know much about them and hence they use them interchangeably, which shouldn't be the case.
   24. Use access specifiers (private, public, protected, internal, protected internal) as per the scope need of a method, a class, or a variable. Let’s say if a class is meant to be used only within the assembly, then it is enough to mark the class as internal only.
   25. Use interfaces wherever needed to maintain decoupling. Some design patterns came into existence due to the usage of interfaces.
   26. Mark a class as sealed or static or abstract as per its usage and your need.
   27. Use a Stringbuilder instead of string if multiple concatenations are required, to save heap memory.
   28. Check whether any unreachable code exists and modify the code if it exists.
   29. Write comments on top of all methods to describe their usage and expected input types and return type information.
   30. Use fiddler/Postman tool to check the HTTP/network traffic and bandwidth information to trace the performance of web application and services.
 */
using reservation_backend.Database;
using reservation_backend.Enums;
using reservation_backend.Exceptions;
using reservation_backend.Interfaces;
using reservation_backend.Users;

namespace reservation_backend.Services;

public class UserService : IUserService
{
    private readonly Context _databaseContext;
    public UserService(Context databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public (RegisterResult, User?) Register(User user, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return (RegisterResult.Empty, null);
        }

        if(!IsValidMailAddress(user.MailAddress))
        {
            return (RegisterResult.InvalidMail, null);
        }

        if (_databaseContext.Users.Any(u => u.MailAddress == user.MailAddress))
        {
            return (RegisterResult.UserMailAlreadyExists, null);
        }
        
        if (_databaseContext.Users.Any(u => u.Username == user.Username))
        {
            return (RegisterResult.UsernameAlreadyExists, null);
        }

        var (hash, salt) = HashService.HashInput(password);

        user.PasswordHash = hash;
        user.PasswordSalt = salt;

        _databaseContext.Users.Add(user);
        _databaseContext.SaveChanges();
        return (RegisterResult.Success, user);
    }
    
    public (LoginResult, User?) Login(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || (string.IsNullOrEmpty(password)))
        {
            return (LoginResult.Empty, null);
        }

        var user = _databaseContext.Users.SingleOrDefault(u => u.MailAddress == email);
        if (user == null)
        {
            return (LoginResult.UserNotFound, null);
        }
        
        if (!HashService.IsHash(password, user.PasswordHash, user.PasswordSalt))
        {
            return (LoginResult.PasswordIncorrect, null);
        }
        
        return (LoginResult.Success, user);
    }
    
    public bool IsValidMailAddress(string emailAddress)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(emailAddress);
            return addr.Address == emailAddress;
        }
        catch
        {
            return false;
        }
    }

}
