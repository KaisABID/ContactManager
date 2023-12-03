using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
namespace ContactManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;
    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        //We verify if the user table is empty or not. If it is we add a default user
        if (_context.Users.Count() == 0)
        {
            User newUser = new User
            {
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@gmail.com",
                Password = "admin1234",
                CreatedAt = DateTime.Now
            };
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser,newUser.Password);
            _context.Add(newUser);
            _context.SaveChanges();
        }
        return View();
    }

    [HttpPost("UserLogin")]
    public IActionResult UserLogin(LoginUser loginUser)
    {

        if (ModelState.IsValid)
            {
            // step 1 : find their email ad if we can't find it throw an error
            User userInDB = _context.Users.FirstOrDefault(a => a.Email == loginUser.LogEmail);
            if (userInDB == null)
                {
                    // there was no Email in the database
                    ModelState.AddModelError("LogEmail", "Invalid Login Attempt");
                    return View("Index");
                }
                PasswordHasher<LoginUser> Hasher = new PasswordHasher<LoginUser>();
                var result = Hasher.VerifyHashedPassword(loginUser, userInDB.Password, loginUser.LogPassword);
                if (result == 0)
                {
                    // this is a problem, we did not the correct password
                    ModelState.AddModelError("LogEmail", "Invalid Login Attempt");
                    return View("Index");
                }
            else
                {
                    HttpContext.Session.SetInt32("UserId", userInDB.UserId);
                    HttpContext.Session.SetString("Email", userInDB.Email);
                    return View("Menu");
                }
            }
        else
            {
                return View("Index");
            }
    }

    public IActionResult Menu()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
