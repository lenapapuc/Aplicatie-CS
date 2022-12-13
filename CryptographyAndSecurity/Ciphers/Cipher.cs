using System;

namespace Lab2
{
    public interface Cipher
    {

        abstract String Encrypt(String message, string key);
        abstract String Decrypt( String message,string key);
        
    }
}