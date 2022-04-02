using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignBookProject.Tools
{
    public class GenerateRandomPassword
    {
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public int RandomNumer(int min, int max)
        {
            Random random = new Random();
            min = 100000;
            max = 1000000;
            return random.Next(min, max);
        }

        public string RandomPassword(int size = 0)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(RandomString(2, true));
            builder.Append(RandomNumer(10000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
    }
}
