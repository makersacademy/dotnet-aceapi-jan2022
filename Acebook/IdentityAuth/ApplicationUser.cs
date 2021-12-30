using System.Collections.Generic;
using Acebook.Models;
using Microsoft.AspNetCore.Identity;

namespace Acebook.IdentityAuth
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Post> Posts { get; set; }

        public ApplicationUserDto ToDto()
        {
            return new ApplicationUserDto
            {
                Id = Id,
                Username = UserName,
            };
        }
    }
}
