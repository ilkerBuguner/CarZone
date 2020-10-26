﻿namespace CarZone.Server.Features.Common
{
    public static class Constants
    {
        public static class Errors
        {
            public const string UnAuthorizedUser = "Unauthorized user!";
            public const string AlreadyRegisteredUserName = "User with username: {0} is alredy registered.";
            public const string InvalidUserName = "Invalid username.";
            public const string InvalidUserId = "Invalid user id.";
            public const string InvalidLoginAttempt = "Invalid username or password.";
            public const string UnAuthorizedRequest = "You don't have access!";
        }
    }
}
