

using System;

namespace TweetApp.Service.ModelDto
{
    public class TweetDto
    {
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime AddedDate { get; set; }
         
        public int UserId { get; set; }
    }
}
