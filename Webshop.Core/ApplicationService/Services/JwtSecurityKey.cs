using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Webshop.Core.ApplicationService.Services
{
    public static class JwtSecurityKey
    {
        private static byte[] secretBytes = Encoding.UTF8.GetBytes("A secret for HmacSha256");

        public static SymmetricSecurityKey Key => new SymmetricSecurityKey(secretBytes);

        public static void SetSecret(string secret)
        {
            secretBytes = Encoding.UTF8.GetBytes(secret);
        }
    }
}