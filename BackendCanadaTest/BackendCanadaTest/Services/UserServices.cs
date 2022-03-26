using BackendCanadaTest.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackendCanadaTest.Services
{
    public class UserServices
    {
        // Create list user
        private readonly List<UserInfor> m_lstUser = new List<UserInfor>()
        {
            new UserInfor()
            {
                UserName = "Canada",
                PassWord = "1Viet1",
                Roles = new List<string>(){"Admin"}
            }
        };

        #region Singleton
        private static UserServices m_objInstance;
        public static UserServices Instance
        {
            get
            {
                if (m_objInstance == null)
                {
                    m_objInstance = new UserServices();
                }
                return m_objInstance;
            }
        }
        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        private UserServices()
        {

        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="x_strUserName">username</param>
        /// <param name="x_strPassWord">password</param>
        public bool LogIn(string x_strUserName, string x_strPassWord)
        {
            bool bExistUser = m_lstUser.Any(x => x.UserName == x_strUserName && x.PassWord == x_strPassWord);
            return bExistUser;
        }

        /// <summary>
        /// Get username
        /// </summary>
        /// <param name="x_strUserName"></param>
        /// <returns></returns>
        public UserInfor GetUser(string x_strUserName)
        {
            return m_lstUser.FirstOrDefault(x => x.UserName == x_strUserName);
        }

        /// <summary>
        /// Genarate authen key for user
        /// </summary>
        /// <param name="x_strUserName"></param>
        /// <returns></returns>
        public bool GenarateAuthenCode(UserInfor x_objUser)
        {
            if (x_objUser != null && string.IsNullOrEmpty(x_objUser.Authen) == true)
            {
                x_objUser.Authen = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Generate JWT token
        /// </summary>
        /// <param name="x_user"></param>
        /// <param name="x_objJWTConfig"></param>
        /// <returns></returns>
        public string GenerateToken(UserInfor x_user, JWTConfig x_objJWTConfig)
        {
            var claims = new List<Claim>(){
                    new Claim(ClaimTypes.Name, x_user.UserName),
            };
            foreach (var role in x_user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(x_objJWTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = x_objJWTConfig.Audience,
                Issuer = x_objJWTConfig.Issuer
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
