using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Models
{

        public class PresentModel
        {
            [Required]
            [Range(1, 100000, ErrorMessage = "Accommodation invalid (1-100000).")]
            public int PresentId { get; set; }
            
            [Required]
            public DateTime Date { get; set; }
            
            [Required]
            public string Url { get; set; }
            public string Price { get; set; }

            [JsonIgnore]
            public bool IsEditing { get; set; }
        }

}