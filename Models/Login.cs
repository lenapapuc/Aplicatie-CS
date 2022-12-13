using System.ComponentModel.DataAnnotations;
using WebApplication3.Hashing_Of_Passwords;

namespace WebApplication3.Models;

public class Login
{
    [Key] 
    public int Id { get; set; }

    [Required]
    [Display(Name = "Username/Email")]
    public string Validation { get; set; }

    private string password;
    [Required]
    [DataType(DataType.Password)]
    public string Password
    {
        get { return SHA256.HashPassword(password);}
        set { password = value; }
    }

    public string UserInput { get; set; }
    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
    public string role { get; set; }
}