namespace CarZone.Server.Features.CarComforts
{
    using System.Threading.Tasks;

    using CarZone.Server.Data.Models.Comfort;
    using CarZone.Server.Features.CarComforts.Models;
    using CarZone.Server.Features.Common.Models;

    public interface ICarComfortsService
    {
        Task<string> CreateAsync(string carId, string comfortId);

        Task UpdateAsync(UpdateCarComfortRequestModel model);

        Task<ResultModel<bool>> DeleteAsync(string carId, string comfortId);

        Task DeleteAllByCarIdAsync(string carId);
    }
}
