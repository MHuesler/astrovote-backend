namespace backend.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public string Analysis { get; set; }
        public int Rating { get; set; }
        public Guid UserFK { get; set; }
    }
}
