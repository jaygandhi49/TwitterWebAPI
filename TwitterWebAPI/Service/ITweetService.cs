using TwitterWebAPI.Models;

namespace TwitterWebAPI.Service
{
    public interface ITweetService
    {
        Task<Response<TweetMaster>> AddTweet(TweetMaster TweetObject, string userName);
        Task<List<TweetMaster>> GetAllTweet();
        Task<bool> DeleteTweet(int TweetId, string userName);
        Task<List<TweetMaster>> GetAllTweetByUser(string userName);
        Task<TweetMaster> UpdateTweet(TweetMaster TweetObject, string userName);
        Task<TweetMaster> GetTweetById(int TweetId);
        Task<Response<TweetAction>> AddTweetLike(string UserName, int TweetId);
        Task<Response<TweetComment>> AddTweetReply(string UserName, int TweetId, bool IntitalComment, string Message);
    }
}
