using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Vote
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public Guid PostFK { get; set; }
        public string UserFK { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
