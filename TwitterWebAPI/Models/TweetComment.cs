using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterWebAPI.Models
{
    public class TweetComment
    {
        public int Id { get; set; }
        public int ParentCommentId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int TweetId { get; set; }
        [Required]
        public string Message { get; set; }
        public virtual UserDetails UserDetails { get; set; }
        public virtual TweetMaster TweetMaster { get; set; }
    }
}
