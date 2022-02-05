using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Ticker { get; set; }
        public string Analysis { get; set; }
        public int Rating { get; set; }
        public string UserFK { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class PostWithUserRating : Post
    {
        public int UserRating { get; set; }
    }
}
