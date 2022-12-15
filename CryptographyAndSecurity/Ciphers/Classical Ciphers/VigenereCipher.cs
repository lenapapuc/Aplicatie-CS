using System;
using System.Text;

namespace CryptographyAndSecurity
{
    public class VigenereCipher : Cipher
    {
        //public static string key = "SUPER";
        
        public string RemoveNonLetters(string plainText)
        {
            StringBuilder newPlainText = new StringBuilder();
            foreach (var element in plainText)
                if (Char.IsLetter(element)) newPlainText.Append(element);
            
            return newPlainText.ToString();
        }

        public string GetKeystream(string plainText, string key)
        {
            StringBuilder keyStream = new StringBuilder();
            int times = RemoveNonLetters(plainText).Length / key.Length;
            for (int i = 0; i < times; i++)
            {
                keyStream.Append(key);
            }

            int rest = RemoveNonLetters(plainText).Length - keyStream.ToString().Length;
            
            for (int i = 0; i < rest; i++)
            {
                keyStream.Append(key[i]);
            }
            
            return keyStream.ToString();
        }

        public string Encrypt(string plainText, string key )
        {
            //PER ASPERA AD ASTRA
            plainText = RemoveNonLetters(plainText);
            string keyStream = GetKeystream(plainText, key);
            StringBuilder encryptedText = new StringBuilder();
            Alphabet alphabet = new Alphabet();
            for (int i = 0; i < plainText.Length; i++)
            {
                int x = (alphabet.Uppercase().IndexOf(plainText[i]) + alphabet.Uppercase().IndexOf(keyStream[i])) % 26;
                encryptedText.Append(alphabet.Uppercase()[x]);
            }

            Console.WriteLine("The encrypted text with the Vigenere Cipher is: " + encryptedText.ToString());
            return encryptedText.ToString();
        }
        
        public string Decrypt(string encryptedText, string key)
        {
            encryptedText = RemoveNonLetters(encryptedText);
            string keyStream = GetKeystream(encryptedText, key);
            StringBuilder decryptedText = new StringBuilder();
            Alphabet alphabet = new Alphabet();
            for (int i = 0; i < encryptedText.Length; i++)
            {
                int x = (alphabet.Uppercase().IndexOf(encryptedText[i]) - alphabet.Uppercase().IndexOf(keyStream[i]) + 26) % 26;
                decryptedText.Append(alphabet.Uppercase()[x]);
            }


            Console.WriteLine("The decrypted text is: " + decryptedText.ToString());
            return decryptedText.ToString();
        }
       
    }
}