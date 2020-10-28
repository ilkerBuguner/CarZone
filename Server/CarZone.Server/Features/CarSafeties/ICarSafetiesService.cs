namespace CarZone.Server.Features.CarSafeties
{
    using System.Threading.Tasks;

    public interface ICarSafetiesService
    {
        Task<string> Create(string carId, string safetyId);
    }
}
