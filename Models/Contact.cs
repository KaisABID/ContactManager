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
    public string Email { get; set; }  

    public string ContactNotes { get; set; }

}