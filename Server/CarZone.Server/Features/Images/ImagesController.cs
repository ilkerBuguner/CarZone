namespace CarZone.Server.Features.Images
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common;
    using CarZone.Server.Features.Common.Models;
    using Microsoft.AspNetCore.Mvc;

    using static CarZone.Server.Infrastructure.ApiRoutes;

    public class ImagesController : ApiController
    {
        private readonly IImagesService imagesService;

        public ImagesController(IImagesService imagesService)
        {
            this.imagesService = imagesService;
        }

        [HttpPost]
        [Route(Image.Create)]
        public async Task<ActionResult> Create(string url, string advertisementId)
        {
            var imageId = await this.imagesService
                .CreateAsync(url, advertisementId);

            return Created(nameof(this.Create), imageId);
        }

        [HttpDelete]
        [Route(Image.Delete)]
        public async Task<ActionResult> Delete(string imageId)
        {
            var deleteRequest = await this.imagesService.DeleteAsync(imageId);

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
