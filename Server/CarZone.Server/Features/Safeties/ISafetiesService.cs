namespace CarZone.Server.Features.Safeties
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Safeties.Models;

    public interface ISafetiesService
    {
        Task<ResultModel<IEnumerable<SafetyListingServiceModel>>> GetAllAsync();
    }
}
