using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_BookStore.Utils
{
    public class utils
    {
        public static string createToken(int length = 20)
        {
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).ToString();
            var listStrRemove = new List<string> { "=", "-", "+", "/" };
            foreach (var c in listStrRemove) token = token.Replace(c, "");
            if (token.Length > length) 
                token = token.Remove(0, token.Length - length);

            return token;
        }
    }
}