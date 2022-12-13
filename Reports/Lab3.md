## Asymmetric Ciphers
### Course: Cryptography & Security
### Author: Papuc Elena

----
## Objectives:
1. Get familiar with the asymmetric cryptography mechanisms.

2. Implement an example of an asymmetric cipher.

## Theory

&ensp;&ensp;&ensp; Asymmetric key cryptosystems / public-key cryptosystems (like RSA, elliptic curve cryptography (ECC), Diffie-Hellman, ElGamal, McEliece, NTRU and others) use a pair of mathematically linked keys: public key (encryption key) and private key (decryption key).

&ensp;&ensp;&ensp; The asymmetric key cryptosystems provide key-pair generation (private + public key), encryption algorithms (asymmetric key ciphers and encryption schemes like RSA-OAEP and ECIES), digital signature algorithms (like DSA, ECDSA and EdDSA) and key exchange algorithms (like DHKE and ECDH).

&ensp;&ensp;&ensp; A message encrypted by the public key is later decrypted by the private key. A message signed by the private key is later verified by the public key. The public key is typically shared with everyone, while the private key is kept secret. Calculating the private key from its corresponding public key is by design computationally infeasible.

## Implementation:

### RSA

&ensp;&ensp;&ensp; The RSA algorithm implementation is divided into 3 distinct 
steps.

&ensp;&ensp;&ensp; For generating the public and private keys, first of all, it is necessary
to choose 2 large prime numbers. For the sake of the laboratory, I chose 2 small prime numbers to be 
able to check the calculations.

1. I calculated the number n which is a result of the multiplication of the previously mentioned
prime numbers (*n = pxq*)
2. I calculated the totient function Phi(n) = (p-1)(q-1)
3. I selected an integer e, such that e is co-prime to the totient function
result and it is larger than 1. I did this by choosing the first e for which the
greatest common divisor between it and the totient function is e
4. The public key is the pair of numbers (n, e)

The code for the aforementioned calculations:

```
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
```

5. The private key generation is done by finding the inverse of the previously calculated 
e modulo Phi:

```
ulong d = InverseModulo(PublicKeyGeneration(), phi);
```
&ensp;&ensp;&ensp; The inverse modulo function is done using the extended Euler Method,
which at each step finds the remainder and the divisor is repeatedly divided by the remainder .
It is done until the remainder is 0.

&ensp;&ensp;&ensp; The encryption is done by taking each character in the message, converting it 
to its ASCII code, and each of that number is raised to the power of e and taken modulo n.
The obtained number is the cipher.

> *c = m<sup>e</sup>mod(n)*

```
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
```
&ensp;&ensp;&ensp; The decryption i s done by the same principle, only instead of the encryption key 
e and n, we have the decryption key d and n.

> *m = c<sup>d</sup>mod(n)*

```
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
```

&ensp;&ensp;&ensp; The GetLargePower method is used to obtain large powers modulo a number.
It is done because the numbers are too large to work with directly, and
there are many errors as a result.

## Conclusions:

&ensp;&ensp;&ensp;  The main benefit of asymmetric ciphers is increased data security. It is the most secure encryption process because users are never required to reveal or share their private keys, thus decreasing the chances of a cybercriminal discovering a user's private key during transmission.