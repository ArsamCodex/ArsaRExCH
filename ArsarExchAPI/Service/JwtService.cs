using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArsarExchAPI.Service
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /*
         * Short Explanation of the GenerateToken Method
                    The GenerateToken method creates a JWT (JSON Web Token) for user authentication and authorization. Here's a breakdown of the key components:

                    Key Generation:

                    SymmetricSecurityKey is created using the JwtSecurityKey from the app configuration.
                    This key is used for signing the token.
                    Signing Credentials:

                    SigningCredentials is initialized with the key and the HMAC-SHA256 algorithm for securing the token.
                    Token Expiry:

                    The token's expiration time is set based on a configurable value (JwtAccessTokenExpiryInMinutes) from the app settings.
                    Token Creation:

                    A JwtSecurityToken is created with:
                    Issuer (JwtIssuer): Identifies the issuing authority.
                    Audience (JwtAudience): Specifies the target audience.
                    Claims: Includes user-specific data.
                    Expiry: Sets the token's validity period.
                    SigningCredentials: Ensures token authenticity.
                    Token Serialization:

                    JwtSecurityTokenHandler.WriteToken serializes the token into a string format for client use.
         * 
         * 
         * 
         * */
        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JwtAccessTokenExpiryInMinutes"));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
