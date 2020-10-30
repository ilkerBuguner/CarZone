namespace CarZone.Server.Features.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.Comments.Models;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Users;
    using CarZone.Server.Features.Users.Models;
    using Microsoft.EntityFrameworkCore;

    using static CarZone.Server.Features.Common.Constants;

    public class CommentsService : ICommentsService
    {
        private readonly CarZoneDbContext dbContext;
        private readonly IUsersService usersService;

        public CommentsService(
            CarZoneDbContext dbContext,
            IUsersService usersService)
        {
            this.dbContext = dbContext;
            this.usersService = usersService;
        }

        public async Task<string> CreateAsync(string content, string advertisementId, string authorId)
        {
            var comment = new Comment
            {
                Content = content,
                AdvertisementId = advertisementId,
                AuthorId = authorId,
            };

            await this.dbContext.Comments.AddAsync(comment);
            await this.dbContext.SaveChangesAsync();

            return comment.Id;
        }

        public async Task<ResultModel<bool>> UpdateAsync(string userId, string commentId, string content)
        {
            var comment = await this.GetByIdAsync(commentId);

            if (comment == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidCommentId },
                };
            }

            if (comment.AuthorId == userId || await this.usersService.IsAdminAsync(userId))
            {
                comment.Content = content;
                comment.ModifiedOn = DateTime.UtcNow;

                this.dbContext.Comments.Update(comment);
                await this.dbContext.SaveChangesAsync();

                return new ResultModel<bool>
                {
                    Success = true,
                };
            }

            return new ResultModel<bool>
            {
                Errors = new string[] { Errors.NoPermissionToEditComment },
            };
        }

        public async Task<ResultModel<bool>> DeleteAsync(string userId, string commentId)
        {
            var comment = await this.GetByIdAsync(commentId);

            if (comment == null)
            {
                return new ResultModel<bool>
                {
                    Errors = new string[] { Errors.InvalidCommentId },
                };
            }

            if (comment.AuthorId == userId || await this.usersService.IsAdminAsync(userId))
            {
                comment.IsDeleted = true;
                comment.DeletedOn = DateTime.UtcNow;

                this.dbContext.Comments.Update(comment);
                await this.dbContext.SaveChangesAsync();

                return new ResultModel<bool>
                {
                    Success = true,
                };
            }

            return new ResultModel<bool>
            {
                Errors = new string[] { Errors.NoPermissionToDeleteComment },
            };
        }

        public async Task<ICollection<CommentDetailsServiceModel>> GetAllByAdvertisementIdAsync(string advertisementId)
        {
            return await this.dbContext
                 .Comments
                 .Where(c => c.AdvertisementId == advertisementId)
                 .Select(c => new CommentDetailsServiceModel
                 {
                     Id = c.Id,
                     Content = c.Content,
                     Likes = c.Likes,
                     Dislikes = c.Dislikes,
                     CreatedOn = c.CreatedOn,
                     Author = new UserInfoServiceModel
                     {
                         Id = c.Author.Id,
                         Username = c.Author.UserName,
                         ProfilePictureUrl = c.Author.ProfilePictureUrl,
                     }
                 })
                 .OrderByDescending(c => c.CreatedOn)
                 .ToListAsync();
        }

        private async Task<Comment> GetByIdAsync(string id)
        {
            return await this.dbContext
                .Comments
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
