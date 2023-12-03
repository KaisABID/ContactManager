#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
namespace ContactManager.Models;

public class Contact
{
    [Key]
    public int ContactId { get; set; } 

    [Required]
    [MinLength(2)]
    public string ContactName { get; set; }
    [Required]

    [EmailAddress]
    public string ContactEmail { get; set; } 

    public string ContactTel1 {get;set;}
    public string ContactTel2 {get;set;}

    public string ContactNotes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [NotMapped]
    public List<ClientContact> ClientContacts { get; set; } = new List<ClientContact>();



}