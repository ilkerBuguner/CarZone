namespace CarZone.Server.Features.Replies
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Replies.Models;

    public interface IRepliesService
    {
        Task<string> CreateAsync(string content, string advertisementId, string rootCommentId, string authorId);

        Task<ResultModel<bool>> UpdateAsync(string userId, string replyId, string content);

        Task<ResultModel<bool>> DeleteAsync(string userId, string replyId);

        Task<ICollection<ReplyDetailsServiceModel>> GetAllByCommentIdAsync(string commentId);

        Task<ResultModel<ReplyDetailsServiceModel>> GetDetailsAsync(string replyId);
    }
}
