namespace CarZone.Server.Infrastructure.Filters
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CarZone.Server.Data.Models;
    using CarZone.Server.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using static CarZone.Server.Data.Common.Constants.Seeding;
    using static CarZone.Server.Features.Common.Constants;

    public class IsAdminAuthorizationAttribute : ActionFilterAttribute, IAsyncActionFilter
    {
        private readonly UserManager<User> userManager;

        public IsAdminAuthorizationAttribute(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is ControllerBase controller)
            {
                var userId = context.HttpContext.User.GetId();
                var user = await userManager.FindByIdAsync(userId);
                var isAdmin = await this.userManager.IsInRoleAsync(user, AdministratorRoleName);

                if (!isAdmin)
                {
                    context.Result = new BadRequestObjectResult(Errors.UnAuthorizedRequest);
                }
                else
                {
                    await next();
                }
            }
        }
    }
}
