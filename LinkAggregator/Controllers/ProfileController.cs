using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LinkAggregator.Data;
using LinkAggregator.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using LinkAggregator.Helpers;

namespace LinkAggregator.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("Profile/Index")]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var links = await this._context.Links.ToListAsync();
            var query = links.Where(x => (DateTime.Now - x.CreationDate) < TimeSpan.FromDays(5));
            query = query.OrderByDescending(s => s.Rating);

            var loggedUser = await this._context.Users.Include(x => x.Links).Include(x => x.Pluses)
                    .FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);
            List<Link> list = query.Where(x => x.UserId == loggedUser.Id).ToList();
            return View(new PaginatedList<Link>(list, pageNumber ?? 1, 100));
        }

        [Authorize]
        [Route("Profile/Create")]
        public IActionResult Create()
        {
            ViewData["Message"] = "Link added.";
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [Route("Profile/Create")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LinkId,Title,Url")] Link link)
        {
            if (ModelState.IsValid)
            {
                link.Rating = 0;
                link.CreationDate = DateTime.UtcNow;
                var loggedUser = await this._context.Users.FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);
                link.UserId = loggedUser.Id;
                _context.Add(link);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Link added.";
                return RedirectToAction(nameof(Index));
            }
            return View(link);
        }
    }
}
