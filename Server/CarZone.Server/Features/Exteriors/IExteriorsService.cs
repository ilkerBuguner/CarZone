namespace CarZone.Server.Features.Exteriors
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Exteriors.Models;

    public interface IExteriorsService
    {
        Task<ResultModel<IEnumerable<ExteriorListingServiceModel>>> GetAllAsync();
    }
}
