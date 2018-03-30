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
        private static long e = 257;

        public static List<BigInteger> encryption(byte[] m, BigInteger n)
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
    }
}
