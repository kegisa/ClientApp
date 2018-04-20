using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;


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

        public static void GeneratePQ()
        {
            RSACryptoServiceProvider rsaCSP = new RSACryptoServiceProvider(1024);

            RSAParameters rsaPrivateParams = rsaCSP.ExportParameters(true);

            p = new BigInteger(rsaPrivateParams.P.Reverse().Concat(new byte[] { 0x00 }).ToArray());
            q = new BigInteger(rsaPrivateParams.Q.Reverse().Concat(new byte[] { 0x00 }).ToArray());
        }

        public static BigInteger Modinv(BigInteger u, BigInteger v)
        {
            BigInteger inv, u1, u3, v1, v3, t1, t3, q;
            BigInteger iter;
            /* Step X1. Initialise */
            u1 = 1;
            u3 = u;
            v1 = 0;
            v3 = v;
            /* Remember odd/even iterations */
            iter = 1;
            /* Step X2. Loop while v3 != 0 */
            while (v3 != 0)
            {
                /* Step X3. Divide and "Subtract" */
                q = u3 / v3;
                t3 = u3 % v3;
                t1 = u1 + q * v1;
                /* Swap */
                u1 = v1; v1 = t1; u3 = v3; v3 = t3;
                iter = -iter;
            }
            /* Make sure u3 = gcd(u,v) == 1 */
            if (u3 != 1)
                return 0;   /* Error: No inverse exists */
                            /* Ensure a positive result */
            if (iter < 0)
                inv = v - u1;
            else
                inv = u1;
            return inv;
        }

        public static void Generate_keys()
        {
            GeneratePQ();
            n = p * q;
            fi = (q - 1) * (p - 1);
            d = Modinv(e, fi);
        }

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

        public static List<BigInteger> EncryptSignature(byte[] hash)
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
        public static BigInteger[] GetOpenKey()
        {
            BigInteger[] openKey = { e, n, d };
            return openKey;
        }
    }
}
