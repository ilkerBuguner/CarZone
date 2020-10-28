namespace CarZone.Server.Features.CarExteriors
{
    using System.Threading.Tasks;

    public interface ICarExteriorsService
    {
        Task<string> CreateAsync(string carId, string exteriorId);
    }
}
