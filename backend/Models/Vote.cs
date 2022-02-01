namespace backend.Models
{
    public class Vote
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public Guid PostFK { get; set; }
    }
}
