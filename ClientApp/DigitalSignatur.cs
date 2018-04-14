using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace ClientApp
{
    class DigitalSignatur
    {
        public static byte[] GetHash()
        {
            //SHA256 mySHA256 = SHA256.Create();
            //SHA256CryptoServiceProvider mySHA256 = new SHA256CryptoServiceProvider();
            //MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();           
            //return md5.ComputeHash(SendingData.ReturnReadFile());
            SHA512CryptoServiceProvider sHA512 = new SHA512CryptoServiceProvider();
            return sHA512.ComputeHash(Connect.readFile);
        }
    }
}
