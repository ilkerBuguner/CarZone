namespace CarZone.Server.Features.Replies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Replies.Models;
    using CarZone.Server.Features.Users;
    using CarZone.Server.Features.Users.Models;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Features.Common.Constants;

    public class RepliesService : IRepliesService
    {
        private readonly CarZoneDbContext dbContext;
        private readonly IUsersService usersService;

        public RepliesService(
            CarZoneDbContext dbContext,
            IUsersService usersService)
        {
            this.dbContext = dbContext;
            this.usersService = usersService;
        }

        public async Task<string> CreateAsync(string content, string advertisementId, string rootCommentId, string authorId)
        {
            var reply = new Reply
            {
                Content = content,
                RootCommentId = rootCommentId,
                AdvertisementId = advertisementId,
                AuthorId = authorId,
            };

            await this.dbContext.Replies.AddAsync(reply);
            await this.dbContext.SaveChangesAsync();

            return reply.Id;
        }

        public async Task<ResultModel<bool>> UpdateAsync(string userId, string replyId, string content)
        {
            var reply = await this.GetByIdAsync(replyId);

            if (reply == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidReplyId },
                };
            }

            if (reply.AuthorId == userId || await this.usersService.IsAdminAsync(userId))
            {
                reply.Content = content;
                reply.ModifiedOn = DateTime.UtcNow;

                this.dbContext.Replies.Update(reply);
                await this.dbContext.SaveChangesAsync();

                return new ResultModel<bool>
                {
                    Success = true,
                };
            }

            return new ResultModel<bool>
            {
                Errors = new string[] { Errors.NoPermissionToEditReply },
            };
        }

        public async Task<ResultModel<bool>> DeleteAsync(string userId, string replyId)
        {
            var reply = await this.GetByIdAsync(replyId);

            if (reply == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidReplyId },
                };
            }

            if (reply.AuthorId == userId || await this.usersService.IsAdminAsync(userId))
            {
                reply.IsDeleted = true;
                reply.DeletedOn = DateTime.UtcNow;

                this.dbContext.Replies.Update(reply);
                await this.dbContext.SaveChangesAsync();

                return new ResultModel<bool>
                {
                    Success = true,
                };
            }

            return new ResultModel<bool>
            {
                Errors = new string[] { Errors.NoPermissionToDeleteReply },
            };
        }

        public async Task<ICollection<ReplyDetailsServiceModel>> GetAllByCommentIdAsync(string commentId)
        {
            return await this.dbContext
                .Replies
                .Where(r => r.RootCommentId == commentId)
                .Select(r => new ReplyDetailsServiceModel
                {
                    Id = r.Id,
                    Content = r.Content,
                    Likes = r.Likes,
                    Dislikes = r.Dislikes,
                    CreatedOn = r.CreatedOn,
                    RootCommentId = commentId,
                    Author = new UserInfoServiceModel
                    {
                         Id = r.Author.Id,
                         Username = r.Author.UserName,
                         ProfilePictureUrl = r.Author.ProfilePictureUrl,
                    }
                })
                .OrderByDescending(r => r.CreatedOn)
                .ToListAsync();
        }

        private async Task<Reply> GetByIdAsync(string id)
        {
            return await this.dbContext
                .Replies
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
