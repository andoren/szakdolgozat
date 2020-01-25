using Iktato;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using System;
using System.Collections.Generic;

namespace IktatogRPCServer.Service
{
    internal class TokenSerivce
    {
        public TokenSerivce()
        {

        }
        private string Secret = "Ótemplomi Szeretetszolgálat";
        internal bool IsValidToken(string token, out User user) {
            user = new User();
            try
            {
                user = GetUserFromJWT(token);
                return true;
            }
            catch (SignatureVerificationException) {
                return false;
            }
            
        }
        internal string GenerateToken(User user) {
            return new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(Secret)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(8).ToUnixTimeSeconds())
                .AddClaim("username", user.Username)
                .AddClaim("privilege", user.Privilege)
                .AddClaim("id",user.Id)
                .Build();
        }
        private User GetUserFromJWT(string jwt) {
            User user = new User();
            IDictionary<string, object> data = new JwtBuilder()
                .WithSecret(Secret)
                .MustVerifySignature()
                .Decode<IDictionary<string, object>>(jwt);
            user.Username = data["username"].ToString();
            user.Privilege = data["privilage"] as Privilege;
            user.Id = int.Parse(data["id"].ToString());
            return user;
        }
    }
}