namespace CarZone.Server.Features.CarExteriors
{
    using System.Threading.Tasks;

    using CarZone.Server.Data.Models.Exterior;
    using CarZone.Server.Features.CarExteriors.Models;
    using CarZone.Server.Features.Common.Models;

    public interface ICarExteriorsService
    {
        Task<string> CreateAsync(string carId, string exteriorId);

        Task UpdateAsync(UpdateCarExteriorRequestModel model);

        Task<ResultModel<bool>> DeleteAsync(string carId, string exteriorId);

        Task DeleteAllByCarIdAsync(string carId);
    }
}
