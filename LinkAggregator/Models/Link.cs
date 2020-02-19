using System;

namespace LinkAggregator.Models
{
    public class Link
    {
        public int LinkId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserID { get; set; }
    }
}