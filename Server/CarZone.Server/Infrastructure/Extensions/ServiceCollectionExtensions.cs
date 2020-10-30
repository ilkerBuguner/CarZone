namespace CarZone.Server.Infrastructure.Extensions
{
    using System;
    using System.Text;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.Advertisements;
    using CarZone.Server.Features.BrandModels;
    using CarZone.Server.Features.Brands;
    using CarZone.Server.Features.CarComforts;
    using CarZone.Server.Features.CarExteriors;
    using CarZone.Server.Features.CarProtections;
    using CarZone.Server.Features.Cars;
    using CarZone.Server.Features.CarSafeties;
    using CarZone.Server.Features.Comforts;
    using CarZone.Server.Features.Comments;
    using CarZone.Server.Features.Exteriors;
    using CarZone.Server.Features.Identity;
    using CarZone.Server.Features.Images;
    using CarZone.Server.Features.Protections;
    using CarZone.Server.Features.Replies;
    using CarZone.Server.Features.Safeties;
    using CarZone.Server.Features.Users;
    using CarZone.Server.Infrastructure.Filters;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;

    public static class ServiceCollectionExtensions
    {
        public static AppSettings GetApplicationSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(applicationSettingsConfiguration);
            return applicationSettingsConfiguration.Get<AppSettings>();
        }

        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddDbContext<CarZoneDbContext>(options => options
                    .UseSqlServer(configuration.GetDefaultConnectionString()));
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<User, Role>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<CarZoneDbContext>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            AppSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<IModelsService, ModelsService>()
                .AddTransient<IComfortsService, ComfortsService>()
                .AddTransient<IExteriorsService, ExteriorsService>()
                .AddTransient<IProtectionsService, ProtectionsService>()
                .AddTransient<ISafetiesService, SafetiesService>()
                .AddTransient<IBrandsService, BrandsService>()
                .AddTransient<ICarComfortsService, CarComfortsService>()
                .AddTransient<ICarExteriorsService, CarExteriorsService>()
                .AddTransient<ICarProtectionsService, CarProtectionsService>()
                .AddTransient<ICarSafetiesService, CarSafetiesService>()
                .AddTransient<ICarsService, CarsService>()
                .AddTransient<IImagesService, ImagesService>()
                .AddTransient<IAdvertisementsService, AdvertisementsService>()
                .AddTransient<IUsersService, UsersService>()
                .AddTransient<ICommentsService, CommentsService>()
                .AddTransient<IRepliesService, RepliesService>()
                .AddScoped<IsAdminAuthorizationAttribute>();
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "My CarZone API",
                        Version = "v1"
                    });
            });
        }
    }
}
