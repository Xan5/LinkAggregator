using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkAggregator.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Link> Links { get; set; }
        public virtual ICollection<Plus> Pluses { get; set; }

        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public User(string name) : this()
        {
            this.UserName = name;
            this.Email = name;
        }
    }
}
