using System;
using System.Text;


namespace CryptographyAndSecurity
{
    public class PolybiusSquareCipher : Cipher
    {
        public string keyRow = "mouse";
        public string keyColumn = "musca";
        public static Alphabet alphabet = new Alphabet();
        string newAlphabet = alphabet.Lowercase().Remove(alphabet.Lowercase().IndexOf('j'), 1);
        StringBuilder encryptedText = new StringBuilder();
        int row, column;
        public string Encrypt(string plainText, string str)
        {
            for (int i = 0; i < plainText.Length; i++)
            {
                
                row = (newAlphabet.IndexOf(plainText[i]) / 5) ;
                column = (newAlphabet.IndexOf(plainText[i]) % 5);

                if (plainText[i] == 'j')
                {
                    row = (newAlphabet.IndexOf('i') / 5) ;
                    column = (newAlphabet.IndexOf('i') % 5) ;
                }

                encryptedText.Append(keyRow[row]);
                encryptedText.Append(keyColumn[column]);
            }
            
            Console.WriteLine("This is the text encrypted with the Polybius Square cipher: " + encryptedText);
            return encryptedText.ToString();
            
        }

        public string Decrypt(string encryptedText, string str)
        {
            StringBuilder decryptedText = new StringBuilder();
            for (int i = 0; i < encryptedText.Length; i++ )
            {
                int index = keyRow.IndexOf(encryptedText[i]) * 5 + keyColumn.IndexOf(encryptedText[i + 1]);
                i++;
                decryptedText.Append(newAlphabet[index]);
            }
            
            Console.WriteLine("This is the text encrypted with the Polybius Square cipher: " + decryptedText);
            return decryptedText.ToString();
        }
        
    }
}