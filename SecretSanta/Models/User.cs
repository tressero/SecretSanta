using System.Collections.Generic;

namespace SecretSanta.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual List<Present> Presents { get; } = new List<Present>();

        // [ForeignKey("UserId")]
        // public virtual User RecipientId { get; set; }
        public int RecipientId { get; set; }
    }
}