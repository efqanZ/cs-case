using System;
using System.Security.Cryptography;
using System.Text;
using CiSeCase.Core.Interfaces.Manager;

namespace CiSeCase.Infrastructure.Managers.Hash
{
    public class Sha256HashManager : IHashManager
    {
        public string GenerateHash(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}