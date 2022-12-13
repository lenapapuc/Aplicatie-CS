using System;
using System.Text;
using static Constants;
namespace Lab2
{
    public class DES : Cipher
    {
        private static string Hex2Binary(string hexvalue)
        {
            int length = hexvalue.Length * 4;
            string binaryval = "";
            binaryval = Convert.ToString(Convert.ToInt64(hexvalue, 16), 2);

            //in order for the obtained string to have the right number of bits, we have to add the missing zeroes in front
            while (binaryval.Length < length)
            {
                binaryval = "0" + binaryval;
            }
            return binaryval;
        }
        private static string Binary2Hex(string binary)
        {
            int length = binary.Length / 4;
            string hexval = "";
            hexval = Convert.ToString(Convert.ToInt64(binary, 2), 16);
            while (hexval.Length < length)
            {
                hexval = "0" + hexval;
            }
           
            return hexval;
        }
        
        public static String Permutation(int[] sequence, String input)
        {
            String output = "";
            input = Hex2Binary(input);
            for (int i = 0; i < sequence.Length; i++)
                output += input[sequence[i] - 1];
            output = Binary2Hex(output);
            return output;
        }
        static String LeftRotate(String str, int d)
        {
            str = Hex2Binary(str);
            //Console.Write(str+ " ");
            String ans = str.Substring(d,str.Length-d) + str.Substring(0, d);
           // Console.WriteLine("this is ans:" + ans);
            ans = Binary2Hex(ans);
            
            return ans;
        }
        
        public static String[] GetKeys(string key)
        {
            String[] keys = new String[16];
            // first key permutation
            key = Permutation(PC1, key);
            
            for (int i = 0; i < 16; i++) {
                //Console.Write(key.Substring(0,7) + " ");
                //Console.WriteLine(key.Substring(7, 7)+ " ");
                //Console.WriteLine("This is the key"+key);
                key = LeftRotate(key.Substring(0, 7), shiftBits[i])
                      + LeftRotate(key.Substring(7, 7), shiftBits[i]);
                //Console.WriteLine("tHIS IS THE PERMUTED KEY" + key);
                // second key permutation
                
                //Console.Write(key + " ");
                keys[i] = Permutation(PC2, key);
               // Console.WriteLine(keys[i]);
            }
            return keys;
        }

        static string Xor(string input, string key)
        {
            input = Hex2Binary(input);
            key = Hex2Binary(key);
            StringBuilder newString = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                newString.Append((input[i] + key[i]) % 2);
            }

            return Binary2Hex(newString.ToString());
        }

        public static String sBoxLookup(String input)
        {
            String output = "";
            input = Hex2Binary(input);
            for (int i = 0; i < 48; i += 6) {
                String temp = input.Substring(i, 6);
                int num = i / 6;
                int row = Convert.ToInt32(temp[0]+ "" + temp[5], 2);
                int col = Convert.ToInt32(temp.Substring(1, 4), 2);
                output += Convert.ToString(sBox[num,row,col], 16);
            }
            return output;
        }
        public static String PracticalCycle(String input, String key, int num)
        {
            // Dividing the input string into 2 parts
            String left = input.Substring(0, 8);
            String temp = input.Substring(8, 8);
            String right = temp;
            // Expansion permutation
            temp = Permutation(EP, temp);
            // xor temp and key
            temp = Xor(temp, key);
            //lookup in s-box table
            temp = sBoxLookup(temp);
            // Straight D-box
            temp = Permutation(P, temp);
            // xor
            left = Xor(left, temp);

            // swapper
            return right + left;
        }

        public string Encrypt(string plainText, string key)
        {
            int i;
            // get 16 keys
            String[] keys = GetKeys(key);
 
            // initial permutation
            plainText = Permutation(IP, plainText);
          
            // 16 rounds
            for (i = 0; i < 16; i++) {
                plainText = PracticalCycle(plainText, keys[i], i);
            }
 
            // last swap
            plainText = plainText.Substring(8, 8) + plainText.Substring(0, 8);
 
            // final permutation
            plainText = Permutation(IP1, plainText);
            return plainText;
        }

        public string Decrypt(string plainText, string key)
        {
            int i;
            // get 16 keys
            String[] keys = GetKeys(key);
 
            // initial permutation
            plainText = Permutation(IP, plainText);
           
            // 16-rounds
            for (i = 15; i > -1; i--) {
                plainText
                    = PracticalCycle(plainText, keys[i], 15 - i);
            }
 
            // 32-bit swap
            plainText = plainText.Substring(8, 8) + plainText.Substring(0, 8);
            plainText = Permutation(IP1, plainText);
            return plainText;
        }
    }
}