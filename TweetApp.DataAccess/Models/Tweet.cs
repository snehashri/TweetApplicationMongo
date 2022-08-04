using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetApp.Domain.Models
{
    public class Tweet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Message { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string User_Id { get; set; }
        public DateTime AddedDate { get; set; }
        public IList<Comment> comments { get; set; }
        public IList<User> Likes { get; set; }



    }
}
