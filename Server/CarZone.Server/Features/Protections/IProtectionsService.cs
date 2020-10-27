namespace CarZone.Server.Features.Protections
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Protections.Models;

    public interface IProtectionsService
    {
        Task<ResultModel<IEnumerable<ProtectionListingServiceModel>>> GetAllAsync();
    }
}
