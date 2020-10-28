namespace CarZone.Server.Features.CarProtections
{
    using System.Threading.Tasks;

    public interface ICarProtectionsService
    {
        Task<string> Create(string carId, string protectionId);
    }
}
