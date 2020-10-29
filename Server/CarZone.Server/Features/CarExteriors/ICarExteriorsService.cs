﻿namespace CarZone.Server.Features.CarExteriors
{
    using System.Threading.Tasks;

    using CarZone.Server.Data.Models.Exterior;
    using CarZone.Server.Features.Common.Models;

    public interface ICarExteriorsService
    {
        Task<string> CreateAsync(string carId, string exteriorId);

        Task<ResultModel<bool>> DeleteAsync(string carId, string exteriorId);

        Task<ResultModel<bool>> DeleteAllByCarIdAsync(string carId);
    }
}
