namespace CarZone.Server.Features.Advertisements
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Advertisements.Models;

    public interface IAdvertisementsService
    {
        Task<string> CreateAsync(string userId, CreateAdvertisementRequestModel input);
    }
}
