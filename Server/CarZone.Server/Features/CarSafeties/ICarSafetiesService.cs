namespace CarZone.Server.Features.CarSafeties
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.CarSafeties.Models;
    using CarZone.Server.Features.Common.Models;

    public interface ICarSafetiesService
    {
        Task<string> CreateAsync(string carId, string safetyId);

        Task UpdateAsync(UpdateCarSafetyRequestModel model);

        Task<ResultModel<bool>> DeleteAsync(string carId, string safetyId);

        Task DeleteAllByCarIdAsync(string carId);
    }
}
