using Microsoft.EntityFrameworkCore;
using TwitterWebAPI.Data;
using TwitterWebAPI.Models;

namespace TwitterWebAPI.Service
{
    public class TweetService : ITweetService
    {
        public readonly TweetDbContext _appDbContext;

        public TweetService(TweetDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Response<TweetMaster>> AddTweet(TweetMaster TweetObject, string userName)
        {
            var response = new Response<TweetMaster>();
            if (string.IsNullOrEmpty(userName))
            {
                response.ErrorMessage = "Username must be required.";
                response.Success = false;
            }
            else
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.LoginId.ToLower() == userName.ToLower());
                if (user == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Username not exist.";
                }
                else
                {
                    var createduser = await _appDbContext.Users.FirstOrDefaultAsync(u => u.LoginId.ToLower() == userName.ToLower());
                    TweetObject.CreatedDate = DateTime.Now;
                    TweetObject.UserId = createduser.Id;
                    _appDbContext.TweetMasters.Add(TweetObject);
                    _appDbContext.SaveChangesAsync();
                    response.Success = true;
                    response.Result = TweetObject;
                }
            }
            return response;
        }

        public async Task<Response<TweetAction>> AddTweetLike(string UserName, int TweetId)
        {
            var response = new Response<TweetAction>();
            if (string.IsNullOrEmpty(UserName))
            {
                response.ErrorMessage = "Username must be required.";
                response.Success = false;
            }
            else
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.LoginId.ToLower() == UserName.ToLower());
                if (user == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Username not exist.";
                }
                else
                {
                    var TweetLikeOject = _appDbContext.TweetLikes.ToList().Count;
                    if (user != null)
                    {
                        TweetAction TweetLike = new TweetAction();
                        TweetLike.UserId = user.Id;
                        TweetLike.TweetId = TweetId;
                        TweetLike.LikeCount = TweetLikeOject + 1;
                        _appDbContext.Add(TweetLike);
                        _appDbContext.SaveChangesAsync();
                        response.Success = true;
                        response.Result = TweetLike;
                    }
                }
            }
            return response;
        }

        public async Task<Response<TweetComment>> AddTweetReply(string UserName, int TweetId, bool IntitalComment, string Message)
        {
            var response = new Response<TweetComment>();
            if (string.IsNullOrEmpty(UserName))
            {
                response.ErrorMessage = "Username must be required.";
                response.Success = false;
            }
            else
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.LoginId.ToLower() == UserName.ToLower());
                if (user == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Username not exist.";
                }
                else
                {
                    var TweetCommentOject = _appDbContext.TweetComments.FirstOrDefault(TweetComment => TweetComment.TweetId == TweetId);
                    TweetComment TweetComment = new TweetComment();
                    if (!IntitalComment && TweetCommentOject != null)
                    {
                        TweetComment.ParentCommentId = TweetCommentOject.Id;
                    }
                    if (user != null)
                    {
                        TweetComment.UserId = user.Id;
                        TweetComment.TweetId = TweetId;
                        TweetComment.Message = Message;
                        _appDbContext.Add(TweetComment);
                        _appDbContext.SaveChangesAsync();
                        response.Success = true;
                        response.Result = TweetComment;
                    }
                }
            }
            return response;           
        }

        public async Task<bool> DeleteTweet(int TweetId, string userName)
        {
            var tweet = await _appDbContext.TweetMasters.FirstOrDefaultAsync(t => t.Id == TweetId);
            _appDbContext.TweetMasters.Remove(tweet);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<TweetMaster>> GetAllTweet()
        {
            try
            {
                return await _appDbContext.TweetMasters.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TweetMaster>> GetAllTweetByUser(string userName)
        {
            var user = _appDbContext.Users.FirstOrDefault(a => a.LoginId == userName);
            return await _appDbContext.TweetMasters.Where(a => a.UserId == user.Id).ToListAsync();
        }

        public async Task<TweetMaster> GetTweetById(int TweetId)
        {
            var tweetObj = _appDbContext.TweetMasters.FirstOrDefaultAsync(a => a.Id == TweetId);
            return await tweetObj;
        }

        public async Task<TweetMaster> UpdateTweet(TweetMaster TweetObject, string userName)
        {
            TweetObject.ModifiedDate = DateTime.Now;
            _appDbContext.TweetMasters.Update(TweetObject);
            _appDbContext.SaveChanges();
            return TweetObject;
        }
    }
}
