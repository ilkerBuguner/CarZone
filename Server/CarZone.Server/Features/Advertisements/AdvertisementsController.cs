namespace CarZone.Server.Features.Advertisements
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Advertisements.Models;
    using CarZone.Server.Features.Common;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;

    using static CarZone.Server.Infrastructure.ApiRoutes;

    public class AdvertisementsController : ApiController
    {
        private readonly IAdvertisementsService advertisementsService;

        public AdvertisementsController(IAdvertisementsService advertisementsService)
        {
            this.advertisementsService = advertisementsService;
        }

        [HttpPost]
        [Route(Advertisement.Create)]
        public async Task<ActionResult> Create(CreateAdvertisementRequestModel model)
        {
            var userId = this.User.GetId();
            model.AuthorId = userId;
            var advertisementId = await this.advertisementsService.CreateAsync(userId, model);

            return Created(nameof(this.Create), advertisementId);
        }

        [HttpDelete]
        [Route(Advertisement.Delete)]
        public async Task<ActionResult> Delete(string advertisementId)
        {
            var userId = this.User.GetId();

            var deleteRequest = await this.advertisementsService.DeleteAsync(userId, advertisementId);

            if (!deleteRequest.Success)
            {
                return this.BadRequest(new ErrorsResponseModel
                {
                    Errors = deleteRequest.Errors,
                });
            }

            return this.Ok();
        }
    }
}
