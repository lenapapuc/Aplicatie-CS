using System;

namespace CryptographyAndSecurity
{
    public interface Cipher
    {

        abstract String Encrypt(String message, string key);
        abstract String Decrypt( String message,string key);
        
    }
}