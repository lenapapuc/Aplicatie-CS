using System;
using System.IO;
using System.Text;
using Lab2;

namespace CryptographyAndSecurity
{ 
    public class CaesarCipher: Cipher
    {
        private static Alphabet cases = new Alphabet();
        public static string UppercaseAlphabet = cases.Uppercase();
        public static string LowercaseAlphabet = cases.Lowercase();
       
        public virtual Char GiveMeChar(char message, int _key)

       {
           int newindex = 0;
           if (Char.IsUpper(message))
           {
               newindex = (UppercaseAlphabet.IndexOf(message) + _key) % UppercaseAlphabet.Length;
               message = UppercaseAlphabet[newindex];
           }
           else
           {
               newindex = (LowercaseAlphabet.IndexOf(message) + _key) % UppercaseAlphabet.Length;
               message = LowercaseAlphabet[newindex];
           }

           return message;
       }

       public string Encrypt(string text, string key)
       {
           string encryptedText = String.Empty;
            
            
           foreach (var character in text)
           {
               if (Char.IsLetter(character))
               {
                   encryptedText += GiveMeChar(character, int.Parse(key));
               }
               else encryptedText += character;
           }
           return encryptedText;
       }
       
       public string Decrypt(string text, string key)
       {
           string decryptedText = String.Empty;
           
           foreach (var character in text)
           {
               if (Char.IsLetter(character))
               {
                   decryptedText += GiveMeChar(character, 26 - int.Parse(key));
               }
               else decryptedText += character;
           }

           return decryptedText;

       }


    }
}
