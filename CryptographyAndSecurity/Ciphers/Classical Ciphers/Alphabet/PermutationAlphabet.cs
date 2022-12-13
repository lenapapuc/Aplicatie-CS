using System;
namespace CryptographyAndSecurity
{
    public class PermutationAlphabet
    {
        private static Alphabet cases = new Alphabet();
        public string CreateNewUpperAlphabet( string permutation_key_upper)
        {
            string UppercaseAlphabet = String.Empty;
            foreach (var member in permutation_key_upper)
                if (!UppercaseAlphabet.Contains(member)) UppercaseAlphabet += member;
            
            foreach (var character in cases.Uppercase()) 
                if (!UppercaseAlphabet.Contains(character)) UppercaseAlphabet += character;
            
            return UppercaseAlphabet;
        }

        public string CreateNewLowerAlphabet(string permutation_key_lower)
        {
            string LowercaseAlphabet = String.Empty;
            foreach (var member in permutation_key_lower)
                if (!LowercaseAlphabet.Contains(member)) LowercaseAlphabet += member;
            
            foreach (var character in cases.Lowercase())
                if (!LowercaseAlphabet.Contains(character)) LowercaseAlphabet += character;
            
            return LowercaseAlphabet;
        }

    }
}