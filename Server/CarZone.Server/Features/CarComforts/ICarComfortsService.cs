namespace CarZone.Server.Features.CarComforts
{
    using System.Threading.Tasks;

    public interface ICarComfortsService
    {
        Task<string> CreateAsync(string carId, string comfortId);
    }
}
