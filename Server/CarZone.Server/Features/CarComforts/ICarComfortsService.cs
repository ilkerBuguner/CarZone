namespace CarZone.Server.Features.CarComforts
{
    using System.Threading.Tasks;

    public interface ICarComfortsService
    {
        Task<string> Create(string carId, string comfortId);
    }
}
