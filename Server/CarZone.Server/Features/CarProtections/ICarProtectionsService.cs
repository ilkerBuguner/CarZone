namespace CarZone.Server.Features.CarProtections
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common.Models;

    public interface ICarProtectionsService
    {
        Task<string> CreateAsync(string carId, string protectionId);

        Task<ResultModel<bool>> DeleteAsync(string carId, string protectionId);

        Task DeleteAllByCarIdAsync(string carId);
    }
}
