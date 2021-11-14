using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ksp.crud.web.api.Helpers
{
    public class HelperToken
    {
        private static Random random = new Random();

        public static string GenerateToken(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
