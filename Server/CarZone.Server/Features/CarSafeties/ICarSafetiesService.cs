namespace CarZone.Server.Features.CarSafeties
{
    using System.Threading.Tasks;

    public interface ICarSafetiesService
    {
        Task<string> CreateAsync(string carId, string safetyId);
    }
}
