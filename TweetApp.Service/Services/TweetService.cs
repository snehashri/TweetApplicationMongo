using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetApp.Domain.Models;
using TweetApp.Domain.Repository;
using TweetApp.Service.ModelDto;
using TweetApp.Service.Response;

namespace TweetApp.Service.Services
{
    public class TweetService : ITweetService
    {
        private readonly IUserRepository _userrepo;
        private readonly ITweetRepository _tweetrepo;
        private readonly IMapper _mapper;

        public TweetService(IUserRepository userrepo, IMapper mapper, ITweetRepository tweetrepo)
        {
            _userrepo = userrepo;
            _tweetrepo = tweetrepo;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<IEnumerable<TweetDto>>> GetAllTweets()
        {
            try {

                var res=_mapper.Map<IEnumerable<TweetDto>>(await _tweetrepo.GetAllTweets());
                foreach (var n in res)
                {
                    int id = n.UserId;
                    var user = await _userrepo.GetUserById(id);
                    n.Email = user.Email;
                }
                return new ServiceResponse<IEnumerable<TweetDto>>()
                {
                    Message = "success",
                    StatusCode = 200,
                    Success = true,
                    Data = res
                };
            }
            catch(Exception ex)
            {
                return new ServiceResponse<IEnumerable<TweetDto>>()
                {
                    Message = ex.ToString(),
                    StatusCode = 401,
                    Success = false,
                    Data = new List<TweetDto>()
                };

            }
        }

        public async Task<ServiceResponse<IEnumerable<TweetDto>>> GetAllTweetsOfUser(int userid)
        {
            try
            {
                var user = await _userrepo.GetUserById(userid);
                var res = _mapper.Map<IEnumerable<TweetDto>>(await _tweetrepo.GetAllTweetsOfUser(userid));
                foreach(var n in res)
                {
                    n.Email = user.Email;
                }
                return new ServiceResponse<IEnumerable<TweetDto>>()
                {
                    Message = "success",
                    StatusCode = 200,
                    Success = true,
                    Data = res
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<TweetDto>>()
                {
                    Message = "something went wrong",
                    StatusCode = 401,
                    Success = false,
                    Data = new List<TweetDto>()
                };

            }
        }

        public async Task<ServiceResponse<IEnumerable<UserDto>>> GetAllUsers()
        {
            try
            {

                var res = _mapper.Map<IEnumerable<UserDto>>(await _userrepo.GetAllUsers());
                
                return new ServiceResponse<IEnumerable<UserDto>>()
                {
                    Message = "success",
                    StatusCode = 200,
                    Success = true,
                    Data = res
                };
            }
            catch (Exception)
            {
            
                return new ServiceResponse<IEnumerable<UserDto>>()
                {
                    Message = "something went wrong",
                    StatusCode = 401,
                    Success = false,
                    Data = new List<UserDto>()
                };

            }
        }

        public async Task<ServiceResponse<bool>> PostTweet(TweetDto tweetmodel)
        {
            try
            {
                if(tweetmodel==null || tweetmodel.Message==null )
                {
                    return new ServiceResponse<bool>()
                    {
                        Message = "Fill all required fields",
                        StatusCode = 400,
                        Success = false,
                        Data = false
                    };

                }
                tweetmodel.AddedDate = DateTime.Now;
                bool res = await _tweetrepo.PostTweet(_mapper.Map<Tweet>(tweetmodel));
                return new ServiceResponse<bool>()
                {
                    Message = "success",
                    StatusCode = 200,
                    Success = true,
                    Data = res
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<bool>()
                {
                    Message = "something went wrong",
                    StatusCode = 401,
                    Success = false,
                    Data = false
                };

            }
        }
    }
}
