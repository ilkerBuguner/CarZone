namespace CarZone.Server.Features.Comments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarZone.Server.Features.Comments.Models;
    using CarZone.Server.Features.Common.Models;

    public interface ICommentsService
    {
        Task<string> CreateAsync(string content, string advertisementId, string authorId);

        Task<ResultModel<bool>> UpdateAsync(string userId, string commentId, string content);

        Task<ResultModel<bool>> DeleteAsync(string userId, string commentId);

        Task<ICollection<CommentDetailsServiceModel>> GetAllByAdvertisementIdAsync(string advertisementId);

        Task<ResultModel<CommentDetailsServiceModel>> GetDetailsAsync(string commentId);
    }
}
