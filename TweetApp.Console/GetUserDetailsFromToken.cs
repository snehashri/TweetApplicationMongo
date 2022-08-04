using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetApp.console
{
    public class GetUserDetailsFromToken
    {
        public string GetEmail(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(token);
            var d = decodedValue.Claims.ToList();
            string email = d[0].Value.ToString();
            return email;
        }

        public int GetUserId(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(token);
            var d = decodedValue.Claims.ToList();
            int id = Int32.Parse(d[1].Value);
            return id;
        }

    }
}
