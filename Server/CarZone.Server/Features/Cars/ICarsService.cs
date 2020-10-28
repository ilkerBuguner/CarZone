namespace CarZone.Server.Features.Cars
{
    using System.Threading.Tasks;

    using CarZone.Server.Features.Cars.Models;

    public interface ICarsService
    {
        Task<string> CreateAsync(CreateCarRequestModel input);

        Task SetCarsAdvertisementAsync(string carId, string advertisementId);
    }
}
