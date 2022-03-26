using BackendCanadaTest.Models;
using BackendCanadaTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendCanadaTest.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserServices m_objLoginServices = UserServices.Instance;
        private readonly JWTConfig m_objJWTConfig;
        public LoginController(IOptions<JWTConfig> x_objJWTConfig)
        {
            m_objJWTConfig = x_objJWTConfig.Value;
        }

        // POST api/<LoginController>
        [AllowAnonymous]
        [HttpPost]
        public void Post([FromBody] LoginData x_objLoginData)
        {
            bool bResult = m_objLoginServices.LogIn(x_objLoginData.UserName, x_objLoginData.Password);
            if (bResult == true)
            {
                // Generate a new google authentication code for the first time.
                GoogleAuthenData objAuthData = null;
                var objUser = m_objLoginServices.GetUser(x_objLoginData.UserName);
                bool bIsGenerateCode = m_objLoginServices.GenarateAuthenCode(objUser);
                if (bIsGenerateCode == true)
                {
                    objAuthData = GoogleAuthenServices.Instance.GetAuthData(objUser);
                }

                Response.StatusCode = StatusCodes.Status200OK;
                Response.ContentType = "application/json";
                Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    GenerateCode = bIsGenerateCode,
                    Image = objAuthData?.QRImage,
                    Key = objAuthData?.ManualKey
                }));
            }
            else
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                Response.ContentType = "application/json";
                Response.WriteAsync("Invalid Username or Password!");
            }
        }

        // GET api/<LoginController>
        [HttpPost("twofapage")]
        public void TwoFAPage([FromBody] TwoFaData x_objData)
        {
            var objUser = m_objLoginServices.GetUser(x_objData.UserName);
            if (objUser != null)
            {
                bool bCheck = GoogleAuthenServices.Instance.VerifyCode(objUser.Authen, x_objData.Code);
                if (bCheck == true)
                {
                    objUser.Token = m_objLoginServices.GenerateToken(objUser, m_objJWTConfig);
                    Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        UserName = objUser.UserName,
                        Token = objUser.Token,
                        Roles = objUser.Roles
                    }));
                }
                else
                {
                    Response.StatusCode = StatusCodes.Status403Forbidden;
                    Response.ContentType = "application/json";
                    Response.WriteAsync("Invalid Google Authenticator code!");
                }
            }
            else
            {
                Response.StatusCode = StatusCodes.Status403Forbidden;
                Response.ContentType = "application/json";
                Response.WriteAsync("Invalid Username!");
            }
        }
    }
}
