using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetApp.Domain.DBContext;
using TweetApp.Domain.Models;

namespace TweetApp.Domain.Repository
{
    public interface ITweetRepository
    {
        Task<IEnumerable<Tweet>> GetAllTweets();
        Task<IEnumerable<Tweet>> GetAllTweetsOfUser(int userid);
        Task<bool> PostTweet(Tweet tweetmodel);
        
       

    }
    public class TweetRepository : ITweetRepository
    {
        private readonly TweetAppDatabaseSettings _context;
        public TweetRepository(TweetAppDatabaseSettings context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Tweet>> GetAllTweets()
        {
            return await _context.Tweets.ToListAsync();
        }

        public async Task<IEnumerable<Tweet>> GetAllTweetsOfUser(int userid)
        {
            var tweetList = await _context.Tweets.Where(s => s.UserId == userid).ToListAsync();
            return tweetList;
        }       
        public async Task<bool> PostTweet(Tweet tweetmodel)
        {
            await _context.Tweets.AddAsync(tweetmodel);
            int Ischanged = await _context.SaveChangesAsync();
            return Ischanged > 0;
        }
       
    }
}
