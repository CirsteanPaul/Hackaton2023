using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HackBackend.Services.Services.Common.Jwt
{
    public class JwtBuilder
    {
        private readonly JwtHeader jwtHeader;
        private readonly JwtPayload jwtPayload;

        public JwtBuilder(IConfiguration configuration)
        {
            jwtHeader = CreateJwtHeader(configuration);
            jwtPayload = CreateJwtPayload(configuration);
        }

        public JwtBuilder AddClaim(Claim claim)
        {
            jwtPayload.AddClaim(claim);
            return this;
        }

        public JwtBuilder AddClaim(string claimName, string claimValue)
        {
            var claim = new Claim(claimName, claimValue);
            jwtPayload.AddClaim(claim);
            return this;
        }

        public string GetToken()
        {
            var jwtToken = new JwtSecurityToken(jwtHeader, jwtPayload);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        private JwtHeader CreateJwtHeader(IConfiguration configuration)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return new JwtHeader(credentials);
        }

        private JwtPayload CreateJwtPayload(IConfiguration configuration)
        {
            var audience = configuration["JwtSettings:Audience"];
            var issuer = configuration["JwtSettings:Issuer"];
            var tokenLifetimeInseconds = int.Parse(configuration["JwtSettings:TokenLifetimeSeconds"]);
            return new JwtPayload(issuer, audience, new List<Claim>(), null, DateTime.Now.AddSeconds(tokenLifetimeInseconds), DateTime.Now);
        }
    }
}
