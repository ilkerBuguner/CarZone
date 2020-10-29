namespace CarZone.Server.Features.CarExteriors
{
    using System.Threading.Tasks;

    using CarZone.Server.Data.Models.Exterior;

    public interface ICarExteriorsService
    {
        Task<string> CreateAsync(string carId, string exteriorId);

        Task DeleteAsync(CarExterior carExterior);
    }
}
