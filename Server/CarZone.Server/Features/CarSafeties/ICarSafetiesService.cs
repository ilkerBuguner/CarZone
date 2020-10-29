namespace CarZone.Server.Features.CarSafeties
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common.Models;

    public interface ICarSafetiesService
    {
        Task<string> CreateAsync(string carId, string safetyId);

        Task<ResultModel<bool>> DeleteAsync(string carId, string safetyId);
    }
}
