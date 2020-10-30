namespace CarZone.Server.Features.Common
{
    public static class Constants
    {
        public static class Errors
        {
            public const string UnAuthorizedUser = "Unauthorized user!";
            public const string AlreadyRegisteredUserName = "User with username: {0} is alredy registered.";
            public const string InvalidUserName = "Invalid username.";
            public const string InvalidUserId = "Invalid user id.";
            public const string InvalidLoginAttempt = "Invalid username or password!";
            public const string UnAuthorizedRequest = "You don't have access!";

            public const string InvalidModelId = "Invalid model id!";
            public const string InvalidBrandId = "Invalid brand id!";
            public const string InvalidCarId = "Invalid car id!";
            public const string InvalidAdvertisementId = "Invalid advertisement id!";
            public const string InvalidImageId = "Invalid image id!";
            public const string InvalidIdsForCarComfort = "Invalid car id or comfort id!";
            public const string InvalidIdsForCarExterior = "Invalid car id or exterior id!";
            public const string InvalidIdsForCarProtection = "Invalid car id or protection id!";
            public const string InvalidIdsForCarSafety = "Invalid car id or safety id!";
            public const string InvalidCommentId = "Invalid comment id!";
            public const string InvalidReplyId = "Invalid reply id!";


            public const string NoPermissionToDeleteAdvertisement = "You must be an Admin or the creator of the advertisement to delete it!";
            public const string NoPermissionToEditComment = "You must be an Admin or the creator of the comment to edit it!";
            public const string NoPermissionToDeleteComment = "You must be an Admin or the creator of the comment to delete it!";

            public const string NoComfortsFound = "No comforts found!";
            public const string NoExteriorsFound = "No exteriors found!";
            public const string NoSafetiesFound = "No safeties found!";
            public const string NoProtectionsFound = "No protections found!";
            public const string NoBrandsFound = "No brands found!";

        }
    }
}
