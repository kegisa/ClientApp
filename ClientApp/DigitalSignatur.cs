
using System.Security.Cryptography;

namespace ClientApp
{
    class DigitalSignatur
    {
        public static byte[] GetHash()
        { 
            SHA512CryptoServiceProvider sHA512 = new SHA512CryptoServiceProvider();
            return sHA512.ComputeHash(Connect.readFile);
        }
    }
}
