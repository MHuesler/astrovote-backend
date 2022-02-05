namespace backend.Resources
{
    public class VoteResource
    {
        public int Rating { get; set; }
        public Guid PostFK { get; set; }
    }
}
