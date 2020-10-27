namespace CarZone.Server.Features.Comforts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarZone.Server.Features.Comforts.Models;
    using CarZone.Server.Features.Common.Models;

    public interface IComfortsService
    {
        Task<ResultModel<IEnumerable<ComfortListingServiceModel>>> GetAllAsync();
    }
}
