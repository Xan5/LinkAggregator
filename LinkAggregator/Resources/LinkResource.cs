using LinkAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkAggregator.Resources
{
    public class LinkResource
    {
        public int LinkId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }

        public string UserId { get; set; }
        public bool PlusButtonVisibility { get; set; }

        public LinkResource(Link link, User user)
        {
            this.LinkId = link.LinkId;
            this.Title = link.Title;
            this.Url = link.Url;
            this.Rating = link.Rating;
            this.CreationDate = link.CreationDate;
            this.UserId = link.UserId;
            if (user != null && (user.Id == link.UserId || user.Pluses.Exists(x => x.LinkId == link.LinkId)))
            {
                this.PlusButtonVisibility = false;
            }
            else
            {
                this.PlusButtonVisibility = true;
            }
        }
    }
}
