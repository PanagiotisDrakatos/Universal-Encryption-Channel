using SecureUWPChannel.IOTransport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EncryptionChannel.Prooperties;

namespace EncryptionChannel.Cryptography
{
    public class PrimeNumberGenerator
    {
       
        private BigInteger publicPrimeNumber;
        private BigInteger privatePrimeNumber;

        private BigInteger g, p;

        public BigInteger ClientPrimeNumber
        {
            get { return publicPrimeNumber; }
            set { publicPrimeNumber = value; }
        }

        public PrimeNumberGenerator()
        {
            g = BigInteger.Parse(SampleConfiguration.exponent);
            p = BigInteger.Parse(SampleConfiguration.modulus);
        }

        public String GetClientPublicNumber()
        {
           
            do
            {
                Random random = new Random();
                byte[] bytes = new byte[256 / 8];
                random.NextBytes(bytes);
                privatePrimeNumber = new BigInteger(bytes);
            }
            while (privatePrimeNumber< 0);
                 
            publicPrimeNumber = BigInteger.ModPow(g,privatePrimeNumber,p);
            return publicPrimeNumber.ToString();
        }

        public String SessionDHGenerator(String ServerResult)
        {
            BigInteger exponent = BigInteger.Parse(ServerResult);
            BigInteger DHresult= BigInteger.ModPow(exponent, privatePrimeNumber, p);
            return DHresult.ToString();
        }

    }
}
