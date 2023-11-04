#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ContactManager.Models;

public class LoginUser
{
    public string LogEmail { get; set; }
    [DataType(DataType.Password)]
    [Required]
    [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
    public string LogPassword { get; set; } 
}