using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SecretSanta.Data.Models
{
    public class Present
    {
       public int PresentId { get; set; } 
       public DateTime Date { get; set; }
       
       public string URL { get; set; }
       public string Price { get; set; }
       public System.Byte[] Image { get; set; }
       
       [ForeignKey("UserId")]
       public virtual User User { get; set; }
    }
}