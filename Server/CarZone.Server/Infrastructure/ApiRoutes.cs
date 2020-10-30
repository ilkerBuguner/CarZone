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
            public const string Create = Root;
            public const string Update = Root + "/{modelId}";
            public const string Delete = Root + "/{modelId}";
            public const string GetDetails = Root + "/{modelId}";
            public const string GetAllByBrandId = Root;
        }

        public static class Comfort
        {
            public const string GetAll = Root;
        }

        public static class Exterior
        {
            public const string GetAll = Root;
        }

        public static class Protection
        {
            public const string GetAll = Root;
        }

        public static class Safety
        {
            public const string GetAll = Root;
        }

        public static class Brand
        {
            public const string GetAll = Root;
        }

        public static class Advertisement
        {
            public const string Create = Root;
            public const string Update = Root + "/{advertisementId}";
            public const string Delete = Root + "/{advertisementId}";
            public const string GetDetails = Root + "/{advertisementId}";
        }

        public static class Comment
        {
            public const string Create = Root;
            public const string Update = Root + "/{commentId}";
            public const string Delete = Root + "/{commentId}";
            public const string GetAllForAdvertisement = Root + "/{advertisementId}";
        }

        public static class Reply
        {
            public const string Create = Root;
            public const string Update = Root + "/{replyId}";
            public const string Delete = Root + "/{replyId}";
            public const string GetAllForComment = Root + "/{commentId}";
        }
    }
}
