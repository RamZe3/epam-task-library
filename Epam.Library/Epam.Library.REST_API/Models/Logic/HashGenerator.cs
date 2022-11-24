using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EPAM.Library.REST_API.Models.Logic
{
    public class HashGenerator
    {
        public string ToSHA512(string s)
        {
            var sb = new StringBuilder();
            using (var sha512 = SHA512.Create())
            {
                byte[] bytes = sha512.ComputeHash(Encoding.Unicode.GetBytes(s));

                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
            }
            return sb.ToString();
        }
    }
}