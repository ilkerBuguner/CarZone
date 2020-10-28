namespace CarZone.Server.Features.CarExteriors
{
    using System.Threading.Tasks;

    public interface ICarExteriorsService
    {
        Task<string> Create(string carId, string exteriorId);
    }
}
