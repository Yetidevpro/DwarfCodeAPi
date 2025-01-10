using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DwarfCodeData.Models
{
    public class AnimeScore
    {
        public int UserId { get; set; }
        public int AnimeId { get; set; }
        public int Score { get; set; }


        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Anime Anime { get; set; }  

       
    }
}



