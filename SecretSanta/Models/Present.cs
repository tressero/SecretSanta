using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecretSanta.Models
{
    public class Present
    {
        public int PresentId { get; set; } 
        public DateTime Date { get; set; }
       
        public string Url { get; set; }
        public string Price { get; set; }
        public byte[] Image { get; set; }
       
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}