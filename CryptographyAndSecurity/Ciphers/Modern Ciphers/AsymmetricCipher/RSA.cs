using System;
using System.Globalization;
using System.Text;
using Lab2;

namespace CryptographyAndSecurity.Modern_Ciphers.AsymmetricCipher
{
    public class RSA : Cipher
    {
        static ulong p = 7;
        static ulong q = 19;
        static ulong phi, n;
        static ulong GCD(ulong num1, ulong num2)
        {
            //Euclidian Algorithm for finding GCD
            ulong remainder;
 
            while (num2 != 0)
            {
                remainder = num1 % num2;
                num1 = num2;
                num2 = remainder;
            }
 
            return num1;
        }
        static ulong InverseModulo(ulong original, ulong modulo)
        {
            double m0 = modulo;
            double p0 = 0, p1 = 1;
 
            if (modulo == 1)
                return 0;
 
            while (original > 1) {
               
                double quotient = original / modulo;
                double temporary = modulo;
                
                modulo = original % modulo;
                original = (ulong) temporary;
                temporary = p0;
                
                if (p0 == 0)
                    p0 = p1;
                else
                {
                    double p;
                    p = (quotient * p0);
                    p0 = p1 - p;
                }

                p1 = temporary;
            }
            
            if (p1 < 0)
                p1 += m0;
           
            return (ulong) p1;
        }

        static ulong PublicKeyGeneration()
        {
            n = p * q;
            phi = (p - 1)*(q - 1);
            ulong e = 2;
            
            while (e < phi) 
            {
                if (GCD(e, phi) == 1)
                    break;
                e++;
            }
            
            return e;
        }

        public ulong GetLargePower(ulong number, ulong power, ulong modulo)
        {
            ulong res = 1; // Initialize result

            number = number % modulo;
            if (number == 0) return 0;

            while (power > 0)
            {
                // If y is odd, multiply x with result
                if ((power % 2) == 1)
                    res = (res * number) % modulo;

                // y must be even now
                power = power >> 1; // y = y/2
                number = (number * number) % modulo; // Change x to x^2
            }

            return res;
        }
        
        public string Encrypt(string message, string key)
        {
            StringBuilder strin = new StringBuilder();
            foreach (var c in message)
            {
                ulong m = (ulong)c;
                double cipherText = GetLargePower(m, PublicKeyGeneration(), n);
                var e = 3 - cipherText.ToString().Length;
                if (e != 0)
                {
                    while (e != 0)
                    {
                        strin.Append("0");
                        e--;
                    }
                    
                }
                strin.Append(cipherText);
            }
            return strin.ToString();
        }

        public string Decrypt(string message, string key)
        {
            ulong d = InverseModulo(PublicKeyGeneration(), phi);
            
            StringBuilder st = new StringBuilder();
            for (int i = 0; i < message.Length; i += 3)
            {
                ulong m = GetLargePower(ulong.Parse(message.Substring(i, 3)), d, n);
                st.Append((char)m);
            }
            
            
            return st.ToString();
        }
    }
}