using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JwtTokenGeneratorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET: /<controller>/
        [HttpPost("Token")]
        public ActionResult GetToken()
        {
            //security key
            var securityKey = "bk2dMQeKVBjgEbG4VS8p";

            //symmetric securitykey
            var symmSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //signing credentials
            var signingCredential = new SigningCredentials(symmSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //setup claim
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            claims.Add(new Claim(ClaimTypes.Role, "ReadOnlyUser"));
            claims.Add(new Claim(ClaimTypes.Role, "ReadWriteUser"));

            //create token
            var token = new JwtSecurityToken(
                issuer: "http://localhost",
                audience: "http://localhost",
                signingCredentials:signingCredential,
                expires: null,
                claims: claims
                );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
