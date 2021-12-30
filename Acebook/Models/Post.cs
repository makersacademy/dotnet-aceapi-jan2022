using System.ComponentModel.DataAnnotations;
using Acebook.IdentityAuth;

namespace Acebook.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public PostDto ToDto()
        {
            return new PostDto
            {
                Id = Id,
                Body = Body,
                User = User?.ToDto(),
            };
        }
    }
}
