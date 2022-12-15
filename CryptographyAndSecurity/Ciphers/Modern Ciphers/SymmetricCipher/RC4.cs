using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CryptographyAndSecurity
{
    public class RC4 : Cipher
    {
        static void SwapNum(ref int x, ref int y)
        {
            int tempswap = x;
            x = y;
            y = tempswap;
        }
        static int[] Sarray()
        {
            int[] S = new int [256];
            for (int i = 0; i < 256; i++)
            {
                S[i] = i;
            }

            return S;
        }

        static int[] KSAalgorithm(byte[] key)
        {
            int[] S = Sarray();
            int[] T = new int[256];
            if (key.Length == 256)
            {
                Buffer.BlockCopy(key, 0, T, 0, key.Length);
            }
            else
            {
                for (int i = 0; i < 256; i++)
                {
                    T[i] = key[i % key.Length];
                }
            }

            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + T[i]) % 256;
                SwapNum(ref S[i], ref S[j]);
            }

            return S;
        }

        static byte[] PRGAlgorithm(int[] S, byte[] plaintext)
        {
            int j = 0;
            int i = 0;
            int k = 0;
            Byte[] result = new byte[plaintext.Length];
            StringBuilder keystream = new StringBuilder();
            for (int l = 0; l < plaintext.Length; l++)
            {
                i = (i + 1) % 256;
                j = (j + S[i]) % 256;
                SwapNum(ref S[i], ref S[j]);
                int t = (S[i] + S[j]) % 256;
                k = S[t];
                result[l] = Convert.ToByte(plaintext[l] ^ k);

            }

            return result;
        }

        public string Encrypt(string message, string key)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            byte[] keyb = Encoding.ASCII.GetBytes((key));
            
            int[] S = KSAalgorithm(keyb);
            StringBuilder result = new StringBuilder();
            foreach (var VARIABLE in PRGAlgorithm(S, data))
            {
                if (VARIABLE.ToString("X").Length == 2) result.Append(VARIABLE.ToString("X"));
                else result.Append("0" + VARIABLE.ToString("X"));

            }

            return result.ToString() ;
        }

        public string Decrypt(string message, string key)
        {
            byte[] keyb = Encoding.ASCII.GetBytes(key);
            byte[] data = new byte[message.Length / 2];
            List<byte> list = new List<byte>();
            int l = 0;
            
            for(int i = 0; i < message.Length; i += 2)
            {
                byte r = Convert.ToByte(message.Substring(i, 2), 16);
                list.Add(r);
            }
            
            foreach (var ele in list)
            {
                data[l] = ele;
                l++;
                
            }
        
            int[] S = KSAalgorithm(keyb);
            return Encoding.ASCII.GetString(PRGAlgorithm(S, data));
        }
        

    }
}