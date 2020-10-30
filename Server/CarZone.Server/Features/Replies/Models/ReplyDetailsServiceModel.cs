namespace CarZone.Server.Features.Replies.Models
{
    using System;

    using CarZone.Server.Features.Users.Models;

    public class ReplyDetailsServiceModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public DateTime CreatedOn { get; set; }

        public string RootCommentId { get; set; }

        public UserInfoServiceModel Author { get; set; }


    }
}
