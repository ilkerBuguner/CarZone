namespace CarZone.Server.Features.Cars
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Cars.Models;
    using CarZone.Server.Features.Common.Models;

    public interface ICarsService
    {
        Task<string> CreateAsync(CreateCarRequestModel input);

        Task<ResultModel<bool>> UpdateAsync(string userId, string carId, UpdateCarRequestModel model);

        Task<ResultModel<bool>> DeleteAsync(string id);

        Task SetCarsAdvertisementAsync(string carId, string advertisementId);
    }
}
