using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.Service.ModelDto;
using TweetApp.Service.Services;
using TweetAppApi.Helpers;
using TweetAppApi.Models;


namespace TweetAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRegistrationService _userregrepo;
        public AuthController(IUserRegistrationService userregrepo)
        {
            _userregrepo = userregrepo;
        }
        
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModelDto usermodel)
        {
            var result=await _userregrepo.UserRegistration(usermodel);
            return Ok(result);


        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModelDto usermodel)
        {
            var result = await _userregrepo.UserLogin(usermodel);
            return Ok(result);


        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModelDto model)
        {
            GetUserDetailsFromToken obj = new GetUserDetailsFromToken();
            string emailid = obj.GetEmail(Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1]);
            model.Email = emailid;
            var result = await _userregrepo.ResetPassword(model);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            GetUserDetailsFromToken obj = new GetUserDetailsFromToken();
            string id = obj.GetUserId(Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1]);
            var result = await _userregrepo.Logout(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgetPasswordModelDto model)
        {
            var result = await _userregrepo.ForgotPasword(model);
            return Ok(result);
        }


    }
}
