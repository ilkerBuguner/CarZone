namespace CarZone.Server.Features.Replies
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Replies.Models;
    using CarZone.Server.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static CarZone.Server.Infrastructure.ApiRoutes;

    public class RepliesController : ApiController
    {
        private readonly IRepliesService repliesService;

        public RepliesController(IRepliesService repliesService)
        {
            this.repliesService = repliesService;
        }

        [HttpPost]
        [Route(Reply.Create)]
        public async Task<ActionResult> Create(CreateReplyRequestModel model)
        {
            var userId = this.User.GetId();

            var replyId = await this.repliesService
                .CreateAsync(model.Content, model.AdvertisementId, model.RootCommentId, userId);

            return Created(nameof(this.Create), replyId);
        }

        [HttpPut]
        [Route(Reply.Update)]
        public async Task<ActionResult> Update(string replyId, [FromBody] UpdateReplyRequestModel model)
        {
            var userId = this.User.GetId();

            var updateRequest = await this.repliesService
                .UpdateAsync(userId, replyId, model.Content);

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
        [Route(Reply.Delete)]
        public async Task<ActionResult> Delete(string replyId)
        {
            var userId = this.User.GetId();

            var deleteRequest = await this.repliesService.DeleteAsync(userId, replyId);

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
        [AllowAnonymous]
        [Route(Reply.GetAllForComment)]
        public async Task<ActionResult<IEnumerable<ReplyDetailsServiceModel>>> GetAllByCommentId(string commentId)
        {
            var commentReplies = await this.repliesService.GetAllByCommentIdAsync(commentId);

            return this.Ok(commentReplies);
        }

        [HttpGet]
        [Route(Reply.GetDetails)]
        public async Task<ActionResult<IEnumerable<ReplyDetailsServiceModel>>> GetDetails(string replyId)
        {
            var detailsRequest = await this.repliesService.GetDetailsAsync(replyId);

            if (!detailsRequest.Success)
            {
                return this.BadRequest(new ErrorsResponseModel
                {
                    Errors = detailsRequest.Errors,
                });
            }

            return this.Ok(detailsRequest.Result);
        }
    }
}
