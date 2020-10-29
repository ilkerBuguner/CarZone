namespace CarZone.Server.Features.Advertisements
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Advertisements.Models;
    using CarZone.Server.Features.Common.Models;

    public interface IAdvertisementsService
    {
        Task<string> CreateAsync(string userId, CreateAdvertisementRequestModel input);

        Task<ResultModel<bool>> DeleteAsync(string userId, string advertisementId);
    }
}
