namespace CarZone.Server.Features.CarComforts
{
    using System.Threading.Tasks;

    using CarZone.Server.Data.Models.Comfort;

    public interface ICarComfortsService
    {
        Task<string> CreateAsync(string carId, string comfortId);

        Task DeleteAsync(CarComfort carComfort);
    }
}
