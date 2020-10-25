namespace CarZone.Server.Infrastructure
{
    using CarZone.Server.Data;
    using CarZone.Server.Data.Common;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
        {
            return app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My CarZone API");
                    options.RoutePrefix = string.Empty;
                });
        }

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<CarZoneDbContext>();

            dbContext.Database.Migrate();
            new DbContextSeeder().SeedAsync(dbContext, services.ServiceProvider).GetAwaiter().GetResult();
        }
    }
}
