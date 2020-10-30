namespace CarZone.Server.Features.Comments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarZone.Server.Features.Comments.Models;
    using CarZone.Server.Features.Common;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;

    using static CarZone.Server.Infrastructure.ApiRoutes;

    public class CommentsController : ApiController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [HttpPost]
        [Route(Comment.Create)]
        public async Task<ActionResult> Create(CreateCommentRequestModel model)
        {
            var userId = this.User.GetId();

            var commentId = await this.commentsService.CreateAsync(model.Content, model.AdvertisementId, userId);

            return Created(nameof(this.Create), commentId);
        }

        [HttpPut]
        [Route(Comment.Update)]
        public async Task<ActionResult> Update(string commentId, [FromBody]UpdateCommentRequestModel model)
        {
            var userId = this.User.GetId();

            var updateRequest = await this.commentsService
                .UpdateAsync(userId, commentId, model.Content);

            if (!updateRequest.Success)
            {
                return this.BadRequest(new ErrorsResponseModel
                {
                    Errors = updateRequest.Errors,
                });
            }

            return this.Ok();
        }

        [HttpDelete]
        [Route(Comment.Delete)]
        public async Task<ActionResult> Delete(string commentId)
        {
            var userId = this.User.GetId();

            var deleteRequest = await this.commentsService.DeleteAsync(userId, commentId);

            if (!deleteRequest.Success)
            {
                return this.BadRequest(new ErrorsResponseModel
                {
                    Errors = deleteRequest.Errors,
                });
            }

            return this.Ok();
        }

        [HttpGet]
        [Route(Comment.GetAllForAdvertisement)]
        public async Task<ActionResult<IEnumerable<CommentDetailsServiceModel>>> GetAllByAdvertisementId(string advertisementId)
        {
            var advertisementComments = await this.commentsService.GetAllByAdvertisementIdAsync(advertisementId);

            return this.Ok(advertisementComments);
        }
    }
}
