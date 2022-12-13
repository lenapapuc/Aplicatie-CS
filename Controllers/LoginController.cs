using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using WebApplication3.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Google.Authenticator;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication3.Controllers;


[ApiController]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public LoginController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpPost]
    [Route("login")]
    public RedirectToActionResult Login(Login login)
    {
        string cmd = null;
        if (login.Validation.IndexOf('@') > -1)
        {
            //Validate email format
            string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(emailRegex);
            if (!re.IsMatch(login.Validation))
            {
                ModelState.AddModelError("Validation", "Email is not valid");
            }
            else cmd = "SELECT * FROM Registration WHERE email ='" + login.Validation + "' AND password_hash = '" +login.Password+ "'";
        }

        else
        {
            string emailRegex = @"^[a-zA-Z0-9]*$";
            Regex re = new Regex(emailRegex);
            if (!re.IsMatch(login.Validation))
            {
                ModelState.AddModelError("Validation", "Username is not valid");
            }

            else cmd = "SELECT * FROM Registration WHERE user_name ='" + login.Validation + "' AND password_hash = '" +login.Password+ "'";
        }
        
        SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DatabaseCon"));
        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd, connection);
        DataTable data = new DataTable();
        dataAdapter.Fill(data);
        if (data.Rows.Count > 0)
        {
            return RedirectToAction("ValidateTwoFactorPin");
        }

        return RedirectToAction("Login");

    }

    [HttpPost]
    [Route("Generate")]
    public SetupCode SecondAuthentication(Login login)
    {
        TwoFactorAuthenticator twoFactorAuthenticator = new TwoFactorAuthenticator(); 
        var two = _configuration["two"];
        var accountSecretKey = $"{two}-{login.Validation}";
        SetupCode setupCode = twoFactorAuthenticator.GenerateSetupCode("Two Factor Demo App", login.Validation, 
            Encoding.ASCII.GetBytes(accountSecretKey));
        return setupCode;
    }

    
    [HttpPost]
    [Route("Verify")]
    public bool ValidateTwoFactorPin([FromBody] Login login)
    {
        var two = _configuration["two"];
        var accountSecretKey = $"{two}-{login.Validation}";
        var twoFactorAuthenticator = new TwoFactorAuthenticator();
        var result = twoFactorAuthenticator
            .ValidateTwoFactorPIN(accountSecretKey, login.UserInput);
        return result;
    }
}