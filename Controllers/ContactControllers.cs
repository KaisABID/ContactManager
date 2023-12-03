using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using System.Linq.Expressions;

public class ContactController : Controller
{
    private readonly ILogger<ContactController> _logger;
    private MyContext _context;

    public ContactController(ILogger<ContactController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult ContactTable()
    {
        // Use pagination or lazy loading instead of ToList() for large datasets
        var allContacts = _context.Contacts.ToList();
        ViewBag.AllContacts=allContacts;
        ViewBag.NbContacts = allContacts.Count;
        return View();
    }
    
    public IActionResult ContactCard()
    {
        return View();
    }

    [HttpGet("EditContact/{idcontact}")]
    
    public IActionResult Editcontact(int idcontact)
    {
        Contact? contact = null;
        if (idcontact != 0 )
        {
            //select conatct with idcontact
            contact = _context.Contacts.FirstOrDefault(c => c.ContactId == idcontact);
            var ListContactsClt = from cc in _context.ClientContacts
                where cc.ContactId == idcontact
                join c in _context.Contacts
                on cc.ContactId equals c.ContactId
                join ct in _context.Clients
                on cc.ClientId equals ct.ClientId
                select new
                {       
                    ClientId = cc.ClientId, 
                    ContactId = cc.ContactId,
                    ClientName = ct.ClientName,
                    ClientEmail = ct.Email
                };
            ViewBag.NbContactClient =ListContactsClt.ToList().Count();
            ViewBag.CltContacts=ListContactsClt.ToList();
            return View("ContactCard",contact);
        }
        else
        {
            ViewBag.AllContacts = _context.Contacts.ToList();
            return View("ContactTable");
        }
    }

    [HttpGet("DeleteContact/{idcontact}")]
    public IActionResult DeleteClt(int idcontact)
    {

        if (idcontact != 0)
        {
            Contact? contact = _context.Contacts.FirstOrDefault(c => c.ContactId == idcontact);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
                return RedirectToAction("ContactTable");
            }
            else
            {
                // Display an error message or handle the situation gracefully
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
        else
        {
            return RedirectToAction("ContactTable");
        }
        
    }
    
    

[HttpPost("ValidContact")]
public IActionResult ValidContact(Contact newcontact)
{
    
    if (ModelState.IsValid)
    {
        if (newcontact.ContactId > 0)
        {
            // Modification de l'élément existant
            Contact OldContact = _context.Contacts.FirstOrDefault(b => b.ContactId == newcontact.ContactId) ;
            if (OldContact != null)
            {
                OldContact.ContactName = newcontact.ContactName;
                OldContact.ContactTel1 = newcontact.ContactTel1;
                OldContact.ContactTel2 = newcontact.ContactTel2;
                OldContact.ContactEmail = newcontact.ContactEmail;
                OldContact.ContactNotes = newcontact.ContactNotes;
                OldContact.UpdatedAt = DateTime.Now;
                _context.Update(OldContact);
                _context.SaveChanges();
            }
        }
        else
        {
            // Ajout d'un nouvel élément
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAA");
            newcontact.CreatedAt = DateTime.Now;
            _context.Contacts.Add(newcontact);
            _context.SaveChanges();
        }

        return RedirectToAction("ContactTable");
    }
    else
    {
        Console.WriteLine("BBBBBBBBBBBB");
        return View("ContactCard");
    }
}

[HttpGet("AffichListClientCt/{IdContact}")]
public IActionResult AffichListContact(int IdContact)
{

    var ListContactClient = from cc in _context.ClientContacts
        where cc.ContactId != IdContact
        join c in _context.Contacts
        on cc.ContactId equals c.ContactId
        join ct in _context.Clients
        on cc.ClientId equals ct.ClientId
        select new
        {       
            ClientId = cc.ClientId, 
            ContactId = cc.ContactId,
            ClientName = ct.ClientName,
            ClientEmail = ct.Email,
            ClientTel1 = ct.Tel1,
            ClientTel2 = ct.Tel2
        };
    ViewBag.NbContactClient =ListContactClient.ToList().Count();
    ViewBag.CltContacts=ListContactClient.ToList();   
    return View("VisionClient");
}


[HttpPost("AddClientToContact/{IdClient}/{IdContact}")]
public IActionResult AddContactToClient(int IdClient,int IdContact)
{
    _context.ClientContacts.Add(new ClientContact
    {
        ClientId = IdClient,
        ContactId = IdContact
    });
    _context.SaveChanges();
    return RedirectToAction("EditContact",IdContact);

}

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public IActionResult Error()
{
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
}