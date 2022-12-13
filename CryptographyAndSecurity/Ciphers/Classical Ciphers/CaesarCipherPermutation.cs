using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using static CryptographyAndSecurity.PermutationAlphabet;

namespace CryptographyAndSecurity
{
    public class CaesarCipherPermutation : CaesarCipher

    {
        public static string permutation_key_lower = "memories";
        public static string permutation_key_upper = "MEMORIES";
        private static PermutationAlphabet _cases = new PermutationAlphabet();
        private string UppercaseAlphabetPermuted = _cases.CreateNewUpperAlphabet(permutation_key_upper);
        private string LowercaseAlphabetPermuted = _cases.CreateNewLowerAlphabet(permutation_key_lower);

        public override char GiveMeChar(char message, int _key)
        {
            int newindex = 0;
            if (Char.IsUpper(message))
            {
                newindex = (UppercaseAlphabetPermuted.IndexOf(message) + _key) % UppercaseAlphabetPermuted.Length;
                message = UppercaseAlphabetPermuted[newindex];
            }
            else
            {
                newindex = (LowercaseAlphabetPermuted.IndexOf(message) + _key) % UppercaseAlphabetPermuted.Length;
                message = LowercaseAlphabetPermuted[newindex];
            }

            return message;
        }
      
    }

}
