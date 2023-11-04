using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Models;

namespace ContactManager.Controllers;

public class ContactsController : Controller
{
    private readonly ILogger<ContactsController> _logger;
    private MyContext _context;
    public ContactsController(ILogger<ContactsController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
