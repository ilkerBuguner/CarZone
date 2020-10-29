namespace CarZone.Server.Features.CarSafeties
{
    using System.Threading.Tasks;

    using CarZone.Server.Data.Models.Safety;

    public interface ICarSafetiesService
    {
        Task<string> CreateAsync(string carId, string safetyId);

        Task DeleteAsync(CarSafety carSafety);
    }
}
