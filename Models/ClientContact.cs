#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ContactManager.Models;

public class ClientContact
{
    [Key]
    public int ClientContactId { get; set; } 

    [Required]
    public int  ClientId { get; set; }
    [Required]

    public int  ContactId { get; set; }  

public Client client {get; set;}
public Contact contact {get; set;}


}
