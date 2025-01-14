using DwarfCodeData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DwarfCodeData.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }

        public string Pasword { get; set; }

        [JsonIgnore]  
        public List<AnimeScore> AnimeScores { get; set; } = new List<AnimeScore>(); 

    }
}
