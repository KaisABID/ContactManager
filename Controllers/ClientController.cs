using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class ClientController : Controller
{
    private readonly ILogger<ClientController> _logger;
    private MyContext _context;

    public ClientController(ILogger<ClientController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult ClientTable()
    {
        // Use pagination or lazy loading instead of ToList() for large datasets
            var allClients = _context.Clients.ToList();

            ViewBag.AllClients=allClients;
            ViewBag.NbClients = allClients.Count;
            return View();
    }
    public IActionResult ClientCard()
    {
        return View();
    }
    [HttpGet("EditClt/{idclient}")]
    
    public IActionResult EditClt(int idclient)
    {
        Client? client = null;
        if (idclient != 0 )
        {
            //select client with idclient
            client = _context.Clients.FirstOrDefault(c => c.ClientId == idclient);
            var ListCltContacts = _context.ClientContacts
                .Include(c => c.contact)
                .Where(c => c.ClientId == idclient)
                .Select(c => new
                {
                    ClientId = c.ClientId,
                    ContactId = c.ContactId,
                    ContactName = c.contact.ContactName,
                    ContactEmail = c.contact.ContactEmail,
                    Contacttel1 = c.contact.ContactTel1,
                    Contacttel2 = c.contact.ContactTel2,
                })
                .ToList();

            ViewBag.NbCltContact =ListCltContacts.Count();
            ViewBag.CltContacts=ListCltContacts;
            ViewBag.ClientId = idclient;
            return View("ClientCard",client);
        }
        else
        {
            ViewBag.AllClients = _context.Clients.ToList();
            ViewBag.ClientId = 0;
            return View("ClientTable");
        }
    }

    [HttpGet("DeleteClt/{idclient}")]
    public IActionResult DeleteClt(int idclient)
    {

        if (idclient != 0)
        {
            Client? client = _context.Clients.FirstOrDefault(c => c.ClientId == idclient);
            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
                return RedirectToAction("ClientTable");
            }
            else
            {
                // Display an error message or handle the situation gracefully
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
        else
        {
            return RedirectToAction("ClientTable");
        }
        
    }
    
    

[HttpPost("ValidClient")]
public IActionResult ValidClient(Client newclient)
{
    if (ModelState.IsValid)
    {
        if (newclient.ClientId > 0)
        {
            // Modification de l'élément existant
            Client OldClient = _context.Clients.FirstOrDefault(b => b.ClientId == newclient.ClientId) ;
            if (OldClient != null)
            {
                OldClient.ClientCode = newclient.ClientCode;
                OldClient.ClientName = newclient.ClientName;
                OldClient.ClientAdress = newclient.ClientAdress;
                OldClient.ClientAdress1 = newclient.ClientAdress1;
                OldClient.Tel1 = newclient.Tel1;
                OldClient.Tel2 = newclient.Tel2;
                OldClient.CP = newclient.CP;
                OldClient.City = newclient.City;
                OldClient.Email = newclient.Email;
                OldClient.ClientNotes = newclient.ClientNotes;
                OldClient.UpdatedAt = DateTime.Now;
                _context.Update(OldClient);
                _context.SaveChanges();
            }
        }
        else
        {
            // Ajout d'un nouvel élément
            
            newclient.CreatedAt = DateTime.Now;
            _context.Clients.Add(newclient);
            _context.SaveChanges();
        }

        return RedirectToAction("ClientTable");
    }
    else
    {

        return View("ClientCard");
    }
}

[HttpGet("AffichListContact/{IdClient}")]
public IActionResult AffichListContact(int idclient)
{ 
    Console.WriteLine("aaaaaaaaaaaaaaaaaaa " + idclient);
    var contactsSansClient = _context.Contacts
    .Where(c => !_context.ClientContacts.Any(cc => cc.ContactId == c.ContactId) || _context.ClientContacts.Any(cc => cc.ContactId == c.ContactId && cc.ClientId != idclient))
            .Select(c => new
            {
                ContactName = c.ContactName,
                ContactEmail = c.ContactEmail,
                ContactTel1 = c.ContactTel1,
                ContactTel2 = c.ContactTel2,
                ContactId = c.ContactId,
            })
            .ToList();

    ViewBag.NbcontactsSansClient = contactsSansClient.Count();
    ViewBag.contactsSansClient = contactsSansClient;
    ViewBag.IdClient = idclient;
    return View("VisionContact");
}

[HttpGet("AddContactToClient/{IdClient}/{idcontact}")]
public IActionResult AddContactToClient(int idclient, int idcontact)
{
if (idcontact!=0 && idclient!=0)
{
    _context.ClientContacts.Add(new ClientContact
    {
        ContactId = idcontact,
        ClientId = idclient
    });
    _context.SaveChanges();
    //return RedirectToAction("EditClt/idclient");
    //return RedirectToAction("ClientController/EditClt/{idclient}");
    return RedirectToAction("EditClt", new { idClient = idclient});
}
else
{
    return RedirectToAction("EditClt", new { idClient =idclient});
}
}

[HttpGet("DeleteContactClt/{idcontact}/{idclient}")]

public IActionResult DeleteContactClt(int idcontact, int idclient)
{
    
    if (idcontact != 0 && idclient != 0)
    {
        ClientContact? contact = _context.ClientContacts.FirstOrDefault(c => c.ContactId == idcontact && c.ClientId == idclient);
        if (contact != null)
        {
            _context.ClientContacts.Remove(contact);
            _context.SaveChanges();
            return RedirectToAction("EditClt", new { idClient = idclient });
        }
        else
        {
            // Display an error message or handle the situation gracefully
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    else
    {
        return RedirectToAction("EditClt", new { idClient = idclient });
    }
}

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public IActionResult Error()
{
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}

}