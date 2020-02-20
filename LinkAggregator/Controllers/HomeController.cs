using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LinkAggregator.Models;
using LinkAggregator.Data;
using LinkAggregator.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using LinkAggregator.Resources;

namespace LinkAggregator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<User> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var links = await this._context.Links.ToListAsync();
            var query = links.Where(x => (DateTime.Now - x.CreationDate) < TimeSpan.FromDays(5));
            query = query.OrderByDescending(s => s.Rating);

            var loggedUser = await this._context.Users.Include(x => x.Links).Include(x => x.Pluses)
                    .FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);
            List<LinkResource> list = new List<LinkResource>();
            foreach (var link in query)
            {
                list.Add(new LinkResource(link, loggedUser));
            }
            return View(new PaginatedList<LinkResource>(list, pageNumber ?? 1, 100));
        }

        [Authorize]
        [HttpGet("{linkId:int}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Plus(int linkId)
        {
            var link = await this._context.Links.FirstOrDefaultAsync(x => x.LinkId == linkId);
            if (link != null)
            {
                link.Rating++;
                var loggedUser = await this._context.Users.Include(x=>x.Links).Include(x=>x.Pluses)
                    .FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);
                if(loggedUser != null && !loggedUser.Links.Exists(x => x.LinkId == linkId) && !loggedUser.Pluses.Exists(x => x.LinkId == linkId))
                {
                    this._context.Pluses.Add(new Plus()
                    {
                        LinkId = linkId,
                        UserId = loggedUser.Id
                    });
                    await this._context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
