namespace CarZone.Server.Features.CarProtections
{
    using System.Threading.Tasks;

    public interface ICarProtectionsService
    {
        Task<string> CreateAsync(string carId, string protectionId);
    }
}
