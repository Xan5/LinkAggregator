namespace LinkAggregator.Models
{
    public class Plus
    {
        public int PlusId { get; set; }
        public string UserId { get; set; }
        public int LinkId { get; set; }

        public User User { get; set; }
        public Link Link { get; set; }
    }
}