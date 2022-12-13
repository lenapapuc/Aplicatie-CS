using System;
using System.Text;

namespace CryptographyAndSecurity
{
    public class Alphabet
    {
        public  string Uppercase()
        {
            StringBuilder alphabetUppercase = new StringBuilder();
               
            for (int i = 65; i <= 90; i++)
            {
                alphabetUppercase.Append(Convert.ToChar(i));
            }

            return alphabetUppercase.ToString();
        }
           
        public  string Lowercase()
        {
            StringBuilder alphabetLowercase = new StringBuilder();
               
            for (int i = 97; i <= 122; i++)
            {
                alphabetLowercase.Append(Convert.ToChar(i));
            }

            return alphabetLowercase.ToString();
        }

    }
}