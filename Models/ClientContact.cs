#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
namespace ContactManager.Models;

public class ClientContact
{
    [Key]
    public int ClientContactId { get; set; } 

    [Required]
    public string ClientId { get; set; }
    [Required]

    public string ContactId { get; set; }  


}
