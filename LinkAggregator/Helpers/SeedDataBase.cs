using LinkAggregator.Data;
using LinkAggregator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkAggregator.Helpers
{
    public class SeedDataBase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }
                User[] users = {
                    new User() {
                        Id = Guid.NewGuid().ToString(),
                        Email = "user1@gmail.com",
                        UserName = "user1@gmail.com",
                        PasswordHash = "AQAAAAEAACcQAAAAEHuZWIg9bRGeof/h5ouKQ3mWtbjy9GW8g5o1MUTJxjToBEIJGXIZrX7Xrz5XE4EDRA=="
                    }, 
                    new User(){
                        Id = Guid.NewGuid().ToString(),
                        Email = "user2@gmail.com",
                        UserName = "user2@gmail.com",
                        PasswordHash = "AQAAAAEAACcQAAAAEHuZWIg9bRGeof/h5ouKQ3mWtbjy9GW8g5o1MUTJxjToBEIJGXIZrX7Xrz5XE4EDRA=="
                    } 
                };
                context.Users.AddRange(users);
                if (context.Links.Any())
                {
                    return;  
                }
                List<Link> links = new List<Link>();
                for (int i = 0; i < 300; i++)
                {
                    links.Add(new Link
                    {
                        Title = "Title" + i.ToString(),
                        CreationDate = DateTime.Now.AddMinutes(-i * 36),
                        Rating = i,
                        Url = "www.numbers.com/" + i.ToString(),
                        UserID = i % 2 == 0 ? users[0].Id : users[1].Id
                    });
                }

                context.Links.AddRange(links.ToArray());
                context.SaveChanges();
            }
        }
    }
}
