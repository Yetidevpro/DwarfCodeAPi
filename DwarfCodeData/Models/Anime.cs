using DwarfCodeData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DwarfCodeData.Models
{
    public class Anime
    {
        public int AnimeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Esta propiedad sigue existiendo, pero no es necesaria para agregar un nuevo anime.
        [JsonIgnore] // Esto asegura que no se serializa al crear o ver un anime
        public List<AnimeScore> AnimeScores { get; set; } = new List<AnimeScore>();
    }
}
