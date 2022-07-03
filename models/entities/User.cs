using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace maker_checker_v1.models.entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public int RoleId { get; set; } = 1;
        public Role Role { get; set; }

        public ICollection<Request>? Requests { get; set; } = new List<Request>();
        public ICollection<Operation>? Operations { get; set; } = new List<Operation>();
        public static byte[] CreateHash(string password)
        {
            using (var sha256 = SHA256.Create())
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        //static method to compare a password and a hash
        public static bool CompareHash(string password, byte[] hash)
        {
            var computedHash = CreateHash(password);
            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != hash[i])
                    return false;
            return true;
        }
        public string generateToken(Claim[] payload, string Issuer, string Audience, string Secret)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));

            var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                           issuer: Issuer,
                           audience: Audience,
                           claims: payload,
                           expires: DateTime.Now.AddMinutes(30),
                           signingCredentials: signInCredentials);

            return (new JwtSecurityTokenHandler()).WriteToken(jwtSecurityToken);
        }

    }
}