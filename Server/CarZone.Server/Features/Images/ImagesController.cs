namespace CarZone.Server.Features.Images
{
    using CarZone.Server.Features.Common;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using static CarZone.Server.Infrastructure.ApiRoutes;

    public class ImagesController : ApiController
    {
        private readonly IImagesService imagesService;

        public ImagesController(IImagesService imagesService)
        {
            this.imagesService = imagesService;
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
