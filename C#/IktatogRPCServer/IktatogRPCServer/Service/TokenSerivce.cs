using Iktato;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using Serilog;
using System;
using System.Collections.Generic;

namespace IktatogRPCServer.Service
{
    internal class TokenSerivce
    {
        public TokenSerivce()
        {

        }
        private readonly string Secret = "Ótemplomi Szeretetszolgálat";
        public  bool IsValidToken(AuthToken token, out User user) {
            user = new User();
            try
            {
                Log.Debug("TokenSerivce.IsValidToken: GetUserFromJWT hívása.");
                user = GetUserFromJWT(token.Token);
                Log.Debug("TokenSerivce.IsValidToken: GetUserFromJWT sikeres hívás.");
                //throw new Exception("Ez egy exception a grpcn keresztül.");
                return true;
            }
            catch (SignatureVerificationException ex)
            {
                Log.Error("TokenService.IsValidtoken: Nem tudtam kiszedni az adatokat a tokenből. {Message}",ex);
                return false;
            }
            catch (Exception e) {
                Log.Error("TokenService.IsValidtoken: Hiba a token validálása közben {Message}", e);
                return false;
            }
            
        }
        public string GenerateToken(User user) {
            return new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(Secret)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(8).ToUnixTimeSeconds())
                .AddClaim("username", user.Username)
                .AddClaim("privilege", user.Privilege.Name)
                .AddClaim("privilegeid", user.Privilege.Id)
                .AddClaim("id",user.Id)
                .Build();
        }
        private User GetUserFromJWT(string jwt) {
            lock (Secret) {
                User user = new User();
                IDictionary<string, object> data = new JwtBuilder()
                    .WithSecret(Secret)
                    .MustVerifySignature()
                    .Decode<IDictionary<string, object>>(jwt);
                user.Username = data["username"].ToString();
                user.Privilege = new Privilege() { Id = int.Parse(data["privilegeid"].ToString()), Name = data["privilege"].ToString() };
                user.Id = int.Parse(data["id"].ToString());
                return user;
            }
        }
    }
}