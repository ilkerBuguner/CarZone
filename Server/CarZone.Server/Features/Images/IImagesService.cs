namespace CarZone.Server.Features.Images
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common.Models;

    public interface IImagesService
    {
        Task<string> CreateAsync(string url, string advertisementId);

        Task<ResultModel<bool>> DeleteAsync(string id);

        Task DeleteAllByAdvertisementIdAsync(string advertisementId);
    }
}
