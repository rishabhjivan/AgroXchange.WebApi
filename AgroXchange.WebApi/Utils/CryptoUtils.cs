using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AgroXchange.WebApi.Utils
{
    public class CryptoUtils
    {
        public static string GenerateRandomUpperCaseAndDigitsString(int length)
        {
            // Create a byte array to hold the random value.
            byte[] randomNumber = new byte[length];

            // Create a new instance of the RNGCryptoServiceProvider. 
            RNGCryptoServiceProvider Gen = new RNGCryptoServiceProvider();

            // Fill the array with a random value.
            Gen.GetBytes(randomNumber);

            string password = "";

            for (int i = 0; i < length; i++)
            {
                int rand = Convert.ToInt32(randomNumber[i]);
                rand = rand % 36;
                if (rand < 10)
                    randomNumber[i] = Convert.ToByte(rand + 48);
                else
                {
                    randomNumber[i] = Convert.ToByte(rand + 55);
                }
                password += Convert.ToString(Convert.ToChar(randomNumber[i]));
            }

            return password;
        }

        public static string GenerateRandomString(int length)
        {
            // Create a byte array to hold the random value.
            byte[] randomNumber = new byte[length];

            // Create a new instance of the RNGCryptoServiceProvider. 
            RNGCryptoServiceProvider Gen = new RNGCryptoServiceProvider();

            // Fill the array with a random value.
            Gen.GetBytes(randomNumber);

            string password = "";

            for (int i = 0; i < length; i++)
            {
                int rand = Convert.ToInt32(randomNumber[i]);
                rand = rand % 62;
                if (rand < 10)
                    randomNumber[i] = Convert.ToByte(rand + 48);
                else
                {
                    if (rand < 36)
                        randomNumber[i] = Convert.ToByte(rand + 55);
                    else
                    {
                        randomNumber[i] = Convert.ToByte(rand + 61);
                    }
                }
                password += Convert.ToString(Convert.ToChar(randomNumber[i]));
            }

            return password;
        }
    }
}
