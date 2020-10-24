namespace CarZone.Server.Data.Common
{
    public static class Constants
    {
        public static class Seeding
        {
            public const string FullName = "Ilko123";

            public const string UserName = "Ilko123";

            public const string Email = "ilko@abv.bg";

            public const string Password = "123456";

            public const string AdministratorRoleName = "Administrator";
        }

        public static class User
        {
            public const int UserNameMinLength = 3;
            public const int UserNameMaxLength = 30;

            public const int FullNameMinLength = 5;
            public const int FullNameMaxLength = 40;

            public const int LocationMinLength = 5;
            public const int LocationMaxLength = 50;
        }

        public static class Advertisement
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;

            public const int DescriptionMinLength = 5;
            public const int DescriptionMaxLength = 500;

            public const int PhoneNumberMinLength = 10;
            public const int PhoneNumberMaxLength = 12;
        }

        public static class Brand
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
        }

        public static class Car
        {
            public const int MinHorsePower = 20;
            public const int MaxHorsePower = 2000;

            public const int MinYear = 1900;

            public const int MinMileage = 0;
            public const int MaxMileage = int.MaxValue;
        }

        public static class Comment
        {
            public const int ContentMinLength = 2;
            public const int ContentMaxLength = 300;
        }

        public static class Reply
        {
            public const int ContentMinLength = 2;
            public const int ContentMaxLength = 300;
        }

        public static class Model
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
        }

        public static class Comfort
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 60;
        }

        public static class Exterior
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 60;
        }

        public static class Protection
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 60;
        }

        public static class Safety
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 60;
        }
    }
}
