﻿using BurnSociety.Application;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BURN_SOCIETY.Models;
using BurnSociety.umbraco.custome_models;
namespace BURN_SOCIETY
{

    public class TokenProvider
    {
        ApplicationDBContext _context;

        public TokenProvider(ApplicationDBContext context)
        {
            _context = context;
        }
        public string LoginUser(string phone, string password)
        {
            // byte[] encodedBytes = System.Text.Encoding.Unicode.GetBytes(password);
            // string encodedTxt = Convert.ToBase64String(encodedBytes);
            //aah hapa..hukuencript pass
            //string username = strEmail;
            string pass = password;
            //string varified = isvarifiead;
            //&& x.IsVarified== "true"
            var user = _context.Register.SingleOrDefault(x => x.Email == phone && x.Password == password);

            //Authenticate User, Check if its a registered user in DB  - JRozario
            if (user == null)
                return null;

            var key = Encoding.ASCII.GetBytes("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
            var JWToken = new JwtSecurityToken(
          issuer: "",
          audience: "",
          claims: GetUserClaims(user),
          notBefore: new DateTimeOffset(DateTime.Now).DateTime,
          expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
          signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));
            var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
            //var strusername = user.Email;
            return token;

        }
        private IEnumerable<Claim> GetUserClaims(Register user)
        {
            List<Claim> claims = new List<Claim>();
            Claim _claim;

            _claim = new Claim(ClaimTypes.Role, user.role.ToString());
            claims.Add(_claim);
            _claim = new Claim(ClaimTypes.Email, user.role.ToString());
            claims.Add(_claim);
            //_claim = new Claim("User_id", user.User_ID.ToString());
            claims.Add(_claim);

            //claims.Add(_claim);
            //_claim = new Claim("EMAILID", user.strEmail);
            //claims.Add(_claim);
            //_claim = new Claim("PHONE", user.strPhone);
            //claims.Add(_claim);
            //_claim = new Claim(ClaimTypes.Name, user.Full_name);
            //claims.Add(_claim);

            //if (user.Role != "")
            //{
            //    _claim = new Claim(user.Role, user.Role);
            //    claims.Add(_claim);
            //}
            return claims.AsEnumerable<Claim>();
        }
    }
}

