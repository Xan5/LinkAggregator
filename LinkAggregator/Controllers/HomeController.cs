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

namespace LinkAggregator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<User> userManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._logger = logger;
        }

        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult Index(int? pageNumber)
        {
            var links = this._context.Links.AsEnumerable().Where(x=>(DateTime.Now - x.CreationDate) < TimeSpan.FromDays(5));

            links = links.OrderByDescending(s => s.Rating);

            return View(new PaginatedList<Link>(links.ToList(), pageNumber ?? 1, 100));
        }

        [HttpPost]
        [Route("Home/Plus/")]
        public async Task<IActionResult> Plus(int linkId)
        {
            var link = await this._context.Links.FirstOrDefaultAsync(x => x.LinkId == linkId);
            if (link != null)
            {
                link.Rating++;
                var logedUser = this._context.Users.FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);
                if (logedUser == null)
                    return NotFound();
                this._context.Pluses.Add(new Plus()
                {
                    LinkId = linkId,
                    UserId = logedUser.Id
                });
                return Ok();
            }
            else
                return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
