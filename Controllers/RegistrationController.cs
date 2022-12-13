using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using WebApplication3.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebApplication3.Controllers;


[ApiController]

public class RegistrationController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public RegistrationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    [Route("registration")]
    public string Registration(Registration registration)
    {
        SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DatabaseCon"));

        string cmd = "INSERT INTO Registration(user_name, password_hash, email, role) VALUES ('" +
                         registration.UserName + "', '" + registration.PasswordHash + "', '" + registration.Email + "', '" + registration.Role + "') ";
        SqlCommand command =
            new SqlCommand(cmd, connection);
        connection.Open();
        int i = command.ExecuteNonQuery();
        connection.Close();
        if (i > 0)
        {
            return "Data Inserted";
        }
        else
        {
            return "Error";
        }
  
    }

   
}