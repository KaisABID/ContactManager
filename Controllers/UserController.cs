using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Models;
using Microsoft.AspNetCore.Identity;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private MyContext _context;

    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult UserTable()
    {
        // Use pagination or lazy loading instead of ToList() for large datasets
        // var allUsers = _context.Users.ToList();
        // ViewBag.AllUsers = allUsers;
        ViewBag.AllUsers = _context.Users.ToList();
        return View();
    }
    
    public IActionResult UserCard()
    {
        return View();
    }

    [HttpGet("Edit/{iduser}")]
    
    public IActionResult Edit(int iduser)
    {
        User? user = null;
        if (iduser != 0 )
        {
            //select user with iduser
            user = _context.Users.FirstOrDefault(u => u.UserId == iduser);
            ViewBag.Pmode ="M";
            return View("UserCard",user);
        }
        else
        {
            ViewBag.AllUsers = _context.Users.ToList();
            ViewBag.Pmode ="A";
            return View("UserTable");
        }
    }

    [HttpGet("Delete/{iduser}")]
    public IActionResult Delete(int iduser)
    {

        if (iduser != 0)
        {
            User? user = _context.Users.FirstOrDefault(u => u.UserId == iduser);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return RedirectToAction("UserTable");
            }
            else
            {
                // Display an error message or handle the situation gracefully
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
        else
        {
            return RedirectToAction("UserTable");
        }
        
    }
    
    

[HttpPost("ValidUser")]
public IActionResult ValidUser(User newuser)
{
    if (ModelState.IsValid)
    {
        if (newuser.UserId > 0)
        {
            // Modification de l'élément existant
            if(_context.Users.Any(u => u.Email == newuser.Email && u.UserId != newuser.UserId))
            {
                ModelState.AddModelError("Email", "Email is already in use!");
                return View("UserCard");
            }
            User OldUser = _context.Users.FirstOrDefault(b => b.UserId == newuser.UserId) ;
            if (OldUser != null)
            {
                OldUser.FirstName = newuser.FirstName;
                OldUser.LastName = newuser.LastName;
                OldUser.Email = newuser.Email;
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                OldUser.Password = Hasher.HashPassword(newuser, newuser.Password);
                OldUser.UpdatedAt = DateTime.Now;
                _context.Update(OldUser);
                _context.SaveChanges();
            }
        }
        else
        {
            if(_context.Users.Any(u => u.Email == newuser.Email))
            {
                ModelState.AddModelError("Email", "Email is already in use!");
                return View("UserCard");
            }
            // Ajout d'un nouvel élément
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newuser.Password = Hasher.HashPassword(newuser, newuser.Password);
            newuser.CreatedAt = DateTime.Now;
            _context.Users.Add(newuser);
            _context.SaveChanges();
        }

        return RedirectToAction("UserTable");
    }
    else
    {
 
        return View("UserCard");
    }
}
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}