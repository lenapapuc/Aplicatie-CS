using System.ComponentModel.DataAnnotations;
using WebApplication3.Hashing_Of_Passwords;

namespace WebApplication3.Models;

public class Registration
{

    public int Id { get; set; }
    
    public string UserName { get; set; }
  
    private string password;
    public string PasswordHash
    {
        get { return SHA256.HashPassword(password);}
        set { password = value; }
    }
    
    [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Please Enter Valid Email")]
    public string Email { get; set; }
    
    public string Role { get; set; }
}