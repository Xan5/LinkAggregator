using System;
using System.Collections.Generic;

namespace LinkAggregator.Models
{
    public class Link
    {
        public int LinkId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public List<Plus> Pluses { get; set; }
    }
}