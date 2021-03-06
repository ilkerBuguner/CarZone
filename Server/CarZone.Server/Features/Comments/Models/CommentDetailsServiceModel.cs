﻿namespace CarZone.Server.Features.Comments.Models
{
    using System;

    using CarZone.Server.Features.Users.Models;

    public class CommentDetailsServiceModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public int RepliesCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserInfoServiceModel Author { get; set; }
    }
}
