using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class RSA
    {
        private static BigInteger e = 257;
        private static BigInteger n;
        private static BigInteger d;
        private static BigInteger fi;
        private static BigInteger p;
        private static BigInteger q;


        public static List<BigInteger> Encryption(byte[] m, BigInteger e, BigInteger n)
        {
            List<BigInteger> encrypted = new List<BigInteger>();
            BigInteger c = new BigInteger();

            for (int i = 0; i < m.Length; i++)
            {
                c = BigInteger.ModPow(m[i], e, n);
                encrypted.Add(c);
            }
            return encrypted;
        }

        public static List<BigInteger> EncryptSignature(byte[] hash, BigInteger d, BigInteger n)
        {
            List<BigInteger> encrypted = new List<BigInteger>();
            BigInteger c = new BigInteger();

            for (int i = 0; i < hash.Length; i++)
            {
                c = BigInteger.ModPow(hash[i], d, n);
                encrypted.Add(c);
            }
            return encrypted;
        }
    }
}
