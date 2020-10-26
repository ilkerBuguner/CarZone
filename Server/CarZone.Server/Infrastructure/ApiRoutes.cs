namespace CarZone.Server.Infrastructure
{
    public class ApiRoutes
    {
        public const string Root = "[controller]";

        public static class Identity
        {
            public const string IdentityRoute = Root + "/identity";
            public const string Login = IdentityRoute + "/login";
            public const string Register = IdentityRoute + "/register";
            public const string UpdateUser = IdentityRoute + "/user";
            public const string GetUser = IdentityRoute + "/user";
        }

        public static class Model
        {
            public const string Base = Root + "/models";

            public const string Create = Base;
            public const string Update = Base + "/{modelId}";
            public const string Delete = Base + "/{modelId}";
        }
    }
}
