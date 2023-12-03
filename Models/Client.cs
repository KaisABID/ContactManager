#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
namespace ContactManager.Models;

public class Client
{
    [Key]
    public int ClientId { get; set; } 
    [Required]
    [MinLength(3)]
    public string ClientCode { get; set; }
    [Required]
    [MinLength(2)]
    public string ClientName { get; set; }
    [Required]
    [MinLength(2)]
    public string ClientAdress { get; set; } 
    public string ClientAdress1 { get; set; } 
    public string City { get; set; }
    public string CP { get; set; }
    public string Tel1 { get; set; }
    public string Tel2 { get; set; }
    [EmailAddress]
    public string Email { get; set; }  
    public string ClientNotes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    [NotMapped]
    public List<ClientContact> ClientContacts { get; set; } = new List<ClientContact>();
}