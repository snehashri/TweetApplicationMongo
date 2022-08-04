using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetApp.Service.ModelDto;
using TweetApp.Service.Response;
using TweetApp.Service.Services;

namespace TweetApp.console
{
    public class TweetApp
    {
        private readonly ITweetService _tweetService;
        private readonly IUserRegistrationService _userService;
        public TweetApp(ITweetService tweetService,IUserRegistrationService userService)
        {
            _tweetService = tweetService;
            _userService = userService;
        }
        public ServiceResponse<IEnumerable<TweetDto>> ViewAllTweets()
        {
            var res= _tweetService.GetAllTweets();
            res.Wait();
            var userRes = res.Result;
            return userRes;

        }
        
        public ServiceResponse<IEnumerable<TweetDto>> ViewAllTweetsOfUser(int id)
        {
            var res = _tweetService.GetAllTweetsOfUser(id);
            res.Wait();
            var userRes = res.Result;
            return userRes;

        }
        public ServiceResponse<bool> PostTweet(TweetDto tweetmodel)
        {
            var res = _tweetService.PostTweet(tweetmodel);
            res.Wait();
            var userRes = res.Result;
            return userRes;

        }
        
        public ServiceResponse<IEnumerable<UserDto>> ViewAllUsers()
        {
            var res = _tweetService.GetAllUsers();
            res.Wait();
            var userRes = res.Result;
            return userRes;

        }

        //Registration Part
        public ServiceResponse<string> UserRegistration(RegisterModelDto usermodel)
        {
            var res =  _userService.UserRegistration(usermodel);
            res.Wait();
            var userRes = res.Result;
            return userRes;
        }
        public ServiceResponse<string> UserLogin(LoginModelDto usermodel)
        {
            
            var res = _userService.UserLogin(usermodel);
            res.Wait();
            var userRes=res.Result;
            return userRes;
        }
        
        public ServiceResponse<string> ForgotPasword(ForgetPasswordModelDto usermodel)
        {
            var res = _userService.ForgotPasword(usermodel);
            res.Wait();
            var userRes = res.Result;
            return userRes;

        }
        public ServiceResponse<string> ResetPassword(ResetPasswordModelDto model)
        {
            var res = _userService.ResetPassword(model);
            res.Wait();
            var userRes = res.Result;
            return userRes;
        }
        public ServiceResponse<string> LogOut(int id)
        {
            var res = _userService.Logout(id);
            res.Wait();
            var userRes = res.Result;
            return userRes;
        }

    }
}
