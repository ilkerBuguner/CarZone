namespace CarZone.Server.Features.CarProtections
{
    using System.Threading.Tasks;

    using CarZone.Server.Data.Models.Protection;

    public interface ICarProtectionsService
    {
        Task<string> CreateAsync(string carId, string protectionId);

        Task DeleteAsync(CarProtection carProtection);
    }
}
