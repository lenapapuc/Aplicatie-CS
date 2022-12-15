using System.Security.Cryptography;
using CryptographyAndSecurity;

using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using RSA = CryptographyAndSecurity.RSA;

namespace WebApplication3.Controllers;

[ApiController]
[Route("[controller]")]
public class EncryptionController: ControllerBase
{
   
    private readonly IConfiguration _configuration;

    public EncryptionController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    [Route("Cesar")]
    public string EncryptCesar([FromQuery] Login login, [FromBody] Encryption encryption)
    {
        if (login.role != "classical") return "User Not Authorized";
        Cipher cesarCipher = new CaesarCipher();
        string ret = cesarCipher.Encrypt(encryption.input, encryption.key);
        return ret;
    }
    
    [HttpPost]
    [Route("RSA")]
    public string EncryptRSA([FromQuery] Login login, [FromBody] Encryption encryption)
    {
        if (login.role != "modern") return "User Not Authorized";
        Cipher rsa = new RSA();
        string ret = rsa.Encrypt(encryption.input, encryption.key);
        return ret;
    }
}