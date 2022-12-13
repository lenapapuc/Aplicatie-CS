# Intro to Cryptography. Classical ciphers. Caesar cipher.

### Course: Cryptography & Security
### Author: Papuc Elena
## Theory<br/>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; In cryptography, a classical cipher is a type of cipher that was used historically but for the most part, has fallen into disuse.</br>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; A cipher uses a system of fixed rules, an encryption algorithm, to transform plaintext, a legible message, into ciphertext, an apparently random string of characters.

### Caesar Cipher
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Caesar Cipher technique is one of the earliest and simplest methods of encryption technique. It’s simply a type of substitution cipher, i.e., each letter of a given text is replaced by a letter with a fixed number of positions down the alphabet.<br>  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Encryption of a letter by a shift *n* can be described mathematically as.

<img alt="E_n(x)=(x+n)mod\ 26" src="https://www.geeksforgeeks.org/wp-content/ql-cache/quicklatex.com-dca1f01b6a20a73c189d48228c230009_l3.svg"/>

### Polybius Square Cipher
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;A Polybius Square is a table that allows someone to convert letters into numbers. 
To make the encryption a little harder, instead of numbers, the square can get one or 2 keywords made up of 5 letters. The characters of these words will substitute each letter of the original text with 2 other letters of the English alphabet
In order to fit the 26 letters of the alphabet into the 25 cells created by the table, the letters ‘i’ and ‘j’ are usually combined into a single cell. Originally there was no such problem because the ancient greek alphabet has 24 letters.

<img src="../Images/Screenshot 2022-09-30 003209.png"/>


### Vigenère Cipher

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Vigenere Cipher is a method of encrypting alphabetic text. It uses a simple form of polyalphabetic substitution. A polyalphabetic cipher is any cipher based on substitution, using multiple substitution alphabets. The encryption of the original text is done using the Vigenère square or Vigenère table.

-   The table consists of the alphabets written out 26 times in different rows, each alphabet shifted cyclically to the left compared to the previous alphabet, corresponding to the 26 possible Caesar Ciphers.
-   At different points in the encryption process, the cipher uses a different alphabet from one of the rows.
-   The alphabet used at each point depends on a repeating keyword.

## Objectives

1. Get familiar with the basics of cryptography and classical ciphers.


2. Implement 4 types of the classical ciphers:

- Caesar cipher with one key used for substitution, 
- Caesar cipher with one key used for substitution, and a permutation of the alphabet,
- Vigenere cipher,
- Polybius Square cipher.

3. Structure the project in methods/classes/packages as neeeded.

## Implementation

- **Caesar Cipher**

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; In order to implement the cipher, I firstly created methods for generating the uppercase
and lowercase alphabets from their letters' respective ASCII codes. I did that in order to be able to encrypt the uppercase letters with uppercase ones and the same with  the lowercase ones.

```
public  string Uppercase()

        {
            StringBuilder alphabetUppercase = new StringBuilder();
               
            for (int i = 65; i <= 90; i++)
            {
                alphabetUppercase.Append(Convert.ToChar(i));
            }

            return alphabetUppercase.ToString();
        }
```

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The encryption and decryption methods read text from the console, 
verify each character from the text, and, if the character is a letter, they apply the encryption formula and return the 
new letter, appending it to the new text. If it's not a letter (it's a white space, symbol, number etc.) they append 
the exact same character to the new text. The difference between the encryption and decryption methods is that they call the 
method that contains the Caesar Cipher formula in different ways. The encryption method calls it as it is:

*E<sub>n</sub>(x) = (x + n) mod 26* (where **n** is the static_key)

```
encryptedText += GiveMeChar(character, static_key);
```

The decryption method calls the same method, but with another key as the decryption formula
*D<sub>n</sub>(x) = (x - n) mod 26* could also be written as <br>

*D<sub>n</sub>(x) = (x + (26-n)) mod 26*
```
encryptedText += GiveMeChar(character, 26 - static_key);
```

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The GiveMeChar() method works by applying the substitution formula
to the 2 alphabets, if the character send to the method is an uppercase letter, it applies it on
the uppercase alphabet, else it applies it to the lowercase one. It returns the new encrypted/decrypted character.

- **Caesar Cipher with permutation of the alphabet**

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Caesar Cipher with a permutation of the alphabet
works the same way as the Caesar Cipher, that is why it inherits the methods from the Caesar Cipher
Class. However, I rewrote the GiveMeChar() Method to overwrite the old one and work with the new alphabet.
```
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
```
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The new alphabet is created by placing a permutation key in front of the old alphabet.
```
public static string permutation_key_lower = "memories";
```
- **Polybius Square Cipher**

&ensp;&ensp;&ensp;&ensp;&ensp; To implement the Polybius Square Cipher, I had 2 methods, one for encryption, and one 
for decryption. The encryption was done by finding the row and column of each letter in the Polybius square.
The keys I use were 
>"mouse" and "musca"

for the row and column respectively. The number of the row was found by simply dividing the index of the letter in the alphabet 
by 5, since there are 5 rows and the letters are arranged by rows.
```
row = (newAlphabet.IndexOf(plainText[i]) / 5) ;
```
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The column was found by finding the remainder of the index of the letter in
the alphabet divided by 5. So, the modulus operation was performed.
```
column = (newAlphabet.IndexOf(plainText[i]) % 5);
```
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The "newAlphabet" variable represents the alphabet I constructed without the letter "j" since it is 
in the same square as "i", therefore it is encrypted the same.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The decryption was done by multiplying the index of the row by 5 and adding the index of the column to it.
````
int index = keyRow.IndexOf(encryptedText[i]) * 5 + keyColumn.IndexOf(encryptedText[i + 1]);
````
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Therefore, each letter is encrypted by 2 other letters, and each 2 letters are decrypted into a single letter.
- **Vigenère Cipher**

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;In order to implement the Vigenere Cipher, I, firstly implemented a method 
to get rid of all the characters that are not letters, therefore obtaining a ongoing string
with no spaces. The method is called *RemoveNonLetters* and it takes as an argument the text for encryption, decryption.

&ensp;&ensp;&ensp;&ensp;&ensp; Then, I implemented a method to get the Keystream, which is 
the key, repeated as many times as needed to obtain a string the length of the original 
text. The method compares the length of the original text to the length of the key and repeats it
as many times as it can entirely an then calculates the difference between the Keystream length at the moment
and the original text length and adds to the Keystream as many letters as needed from the key to have 
the desired number of letters in the Keystream.
```
 public string GetKeystream(string plainText)
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
```

&ensp;&ensp;&ensp;&ensp;&ensp; Finally, the encrypt and decrypt methods work by applying the formulas:

*E<sub>i</sub> = (P<sub>i</sub> + K<sub>i</sub>) mod 26*

*D<sub>i</sub> = (E<sub>i</sub> - K<sub>i</sub> + 26) mod 26*

&ensp;&ensp;&ensp;&ensp;&ensp; The "P" represents the character from the original text, the "K" - the one from the Keystream,
the "E" represents the encrypted character, and "D" - the decrypted one. The "i", or the index of the character is taken from the
place of the letter in the uppercase alphabet that was already created. I chose to work only with Uppercase strings for this cipher.
The "+ 26" in the last formula is added so we wouldn't get negative indexes. This modification of the formula can be done since 26 mod 26 is 0, and 0 is a neutral element in addition
operations, .

```
for (int i = 0; i < plainText.Length; i++)
            {
                int x = (alphabet.Uppercase().IndexOf(plainText[i]) + alphabet.Uppercase().IndexOf(keyStream[i])) % 26;
                encryptedText.Append(alphabet.Uppercase()[x]);
            }
```
&ensp;&ensp;&ensp;&ensp;&ensp; The result of encrypting with the key "SUPER"

<img alt="image" src="../Images/Screenshot 2022-09-29 230523.png"/>