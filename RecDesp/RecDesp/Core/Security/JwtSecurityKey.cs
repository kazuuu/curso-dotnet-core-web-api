using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace RecDesp.Core.Security
{
    public class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
