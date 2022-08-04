using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetApp.Service.ModelDto;
using TweetApp.Service.Response;

namespace TweetApp.Service.Services
{
    public interface ITweetService
    {
        Task<ServiceResponse<IEnumerable<TweetDto>>> GetAllTweets();
        Task<ServiceResponse<IEnumerable<TweetDto>>> GetAllTweetsOfUser(int userid);
        Task<ServiceResponse<bool>> PostTweet(TweetDto tweetmodel);
        Task<ServiceResponse<IEnumerable<UserDto>>> GetAllUsers();



    }
}
