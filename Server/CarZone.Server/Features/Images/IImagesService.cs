namespace CarZone.Server.Features.Images
{
    using System.Threading.Tasks;

    public interface IImagesService
    {
        Task<string> CreateAsync(string url, string advertisementId);
    }
}
