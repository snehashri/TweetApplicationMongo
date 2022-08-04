using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp.Service.ModelDto;
using TweetApp.Service.Services;
using TweetAppApi.Helpers;

namespace TweetAppApi.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        
        private readonly ITweetService _tweetService;
        public TweetController(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _tweetService.GetAllUsers();
            return Ok(result);
        }
        [HttpGet]
        [Route("GetAllTweets")]
        public async Task<IActionResult> GetAllTweets()
        {
            var result = await _tweetService.GetAllTweets();
            return Ok(result);
        }
        
        [HttpGet]
        [Route("GetAllTweetsofUser")]
        public async Task<IActionResult> GetAllTweetsofUser()
        {
            GetUserDetailsFromToken obj = new GetUserDetailsFromToken();
            int id= obj.GetUserId(Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1]);            
            var result = await _tweetService.GetAllTweetsOfUser(id);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("PostTweet")]
        public async Task<IActionResult> PostTweet(TweetDto tweetmodel)
        {
            GetUserDetailsFromToken obj = new GetUserDetailsFromToken();
            int id = obj.GetUserId(Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1]);
            string emailid = obj.GetEmail(Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1]);
            tweetmodel.UserId = id;
            tweetmodel.Email = emailid;
            var result = await _tweetService.PostTweet(tweetmodel);
            return Ok(result);
        }

       


    }
}
