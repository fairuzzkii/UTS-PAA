using API_Dealer_Mobil_Acc.Helper;
using API_Dealer_Mobil_Acc.Models;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Dealer_Mobil_Acc.Context
{
    public class LoginContext
    {
        private readonly sqlDBHelper _db;
        private readonly IConfiguration _config;

        public LoginContext(string constr, IConfiguration config)
        {
            _db = new sqlDBHelper(constr);
            _config = config;
        }

        public List<Login> Autentifikasi(string username, string password)
        {
            var list = new List<Login>();

            try
            {
                string query = @"
                    SELECT u.username, r.nama_role
                    FROM users u
                    JOIN user_roles ur ON u.id = ur.user_id
                    JOIN roles r ON ur.role_id = r.id
                    WHERE u.username = @username AND u.password = @password
                ";

                using var cmd = _db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string user = reader.GetString(0);
                    string role = reader.GetString(1);

                    var login = new Login
                    {
                        Username = user,
                        Role = role,
                        Token = GenerateJwtToken(user, role)
                    };

                    list.Add(login);
                }

                reader.Close();
                return list;
            }
            catch (Exception ex)
            {

                return new List<Login>();
            }
            finally
            {
                _db.closeConnection();
            }
        }

        private string GenerateJwtToken(string username, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
