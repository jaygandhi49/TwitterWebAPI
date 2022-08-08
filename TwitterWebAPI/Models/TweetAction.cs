using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterWebAPI.Models
{
    public class TweetAction
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int TweetId { get; set; }
        public int LikeCount { get; set; }

        public virtual UserDetails UserDetails { get; set; }
        public virtual TweetMaster TweetMaster { get; set; }
    }
}
