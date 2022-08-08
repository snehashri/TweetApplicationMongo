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
using TweetApp.Service.Response;
using TweetApp.Service.Services;
using TweetAppApi.Helpers;

namespace TweetAppApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Route("GetTweets/{username}")]
        public async Task<IActionResult> GetTweetsByUsername([FromRoute] string username)
        {
            var result = await _tweetService.GetTweetsByUserName(username);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllTweetsofUser")]
        public async Task<IActionResult> GetTweetsByUserId()
        {
            GetUserDetailsFromToken obj = new GetUserDetailsFromToken();
            string id= obj.GetUserId(Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1]);            
            var result = await _tweetService.GetTweetsByUserId(id);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("PostTweet")]
        public async Task<IActionResult> PostTweet(TweetDto tweetmodel)
        {
            GetUserDetailsFromToken obj = new GetUserDetailsFromToken();
            string id = obj.GetUserId(Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1]);
            string emailid = obj.GetEmail(Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1]);
            tweetmodel.UserId = id;
            tweetmodel.Email = emailid;
            var result = await _tweetService.PostTweet(tweetmodel);
            return Ok(result);
        }

        [HttpPost]
        [Route("reply/{id}")]
        public async Task<IActionResult> ReplyTweet( [FromRoute]string id,[FromBody]string message)
        {
            GetUserDetailsFromToken obj = new GetUserDetailsFromToken();
            string username = obj.GetUsername(Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1]);
            var result = await _tweetService.ReplyTweet(username,id,message);
            return Ok(result);
        }

        [HttpPut]
        [Route("like/{id}")]
        public async Task<IActionResult> LikeTweet( [FromRoute] string id)
        {
            GetUserDetailsFromToken obj = new GetUserDetailsFromToken();
            string username = obj.GetUsername(Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1]);
            var result = await _tweetService.LikeTweet(username,id);
            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateTweet( [FromRoute] string id, TweetDto tweetmodel)
        {
            GetUserDetailsFromToken obj = new GetUserDetailsFromToken();
            string username = obj.GetUsername(Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1]);
            var result = await _tweetService.UpdateTweet(id,username,tweetmodel);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTweet( [FromRoute] string id)
        {
            var result = await _tweetService.DeleteTweet(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetIdFromToken")]
        public async Task<IActionResult> GetIdFromToken()
        {
            GetUserDetailsFromToken obj = new GetUserDetailsFromToken();
            string id = obj.GetUserId(Request.Headers[HeaderNames.Authorization].ToString().Split(" ")[1]);
            var res = new ServiceResponse<string>()
            {
                Data = id,
                StatusCode = 200,
                Message = "Success",
                Success = true
            };
            return Ok(res);
        }






    }
}
