
using DwarfCodeData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AnimeController : ControllerBase
{
    private readonly AppDbContext _context;

    public AnimeController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Anime
    [HttpGet]
    public ActionResult<IEnumerable<Anime>> GetAllAnimes()
    {
        var animes = _context.Animes
            .Include(a => a.AnimeScores)
            .ThenInclude(au => au.User)
            .ToList();

        return Ok(animes);
    }

    // GET: api/Anime/{id}
    [HttpGet("{id}")]
    public ActionResult<Anime> GetAnimeById(int id)
    {
        var anime = _context.Animes
            .Include(a => a.AnimeScores)
            .ThenInclude(au => au.User)
            .FirstOrDefault(a => a.AnimeId == id);

        if (anime == null)
        {
            return NotFound("Anime not found.");
        }

        return Ok(anime);
    }

    // POST: api/Anime
    [HttpPost]
    public ActionResult<Anime> AddAnime([FromBody] Anime anime)
    {
        if (anime == null)
        {
            return BadRequest("Invalid data.");
        }

        _context.Animes.Add(anime);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetAnimeById), new { id = anime.AnimeId }, anime);
    }




    // PUT: api/Anime/{id}
    [HttpPut("{id}")]
    public ActionResult EditAnime(int id, [FromBody] Anime anime)
    {
        var existingAnime = _context.Animes.Find(id);
        if (existingAnime == null)
        {
            return NotFound("Anime not found.");
        }

        existingAnime.Name = anime.Name;
        existingAnime.Description = anime.Description;
        _context.SaveChanges();
        return NoContent();
    }

    // DELETE: api/Anime/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteAnime(int id)
    {
        var anime = _context.Animes.Find(id);
        if (anime == null)
        {
            return NotFound("Anime not found.");
        }

        _context.Animes.Remove(anime);
        _context.SaveChanges();
        return NoContent();
    }

    // GET: api/Anime/{animeId}/Users
    [HttpGet("{animeId}/Users")]
    public ActionResult<IEnumerable<object>> GetUsersForAnime(int animeId)
    {
        var anime = _context.Animes
            .Include(a => a.AnimeScores) 
            .ThenInclude(sc => sc.User) 
            .FirstOrDefault(a => a.AnimeId == animeId);

        if (anime == null)
        {
            return NotFound("Anime not found.");
        }

        var usersWithScores = anime.AnimeScores
            .Select(sc => new
            {
                User = sc.User,
                Score = sc.Score
            })
            .ToList();

        return Ok(usersWithScores);
    }


    // POST: api/Anime/{animeId}/User/{userId}
    [HttpPost("{animeId}/User/{userId}")]
    public async Task<ActionResult> AddUserToAnime(int animeId, int userId, [FromBody] int score)
    {
        var anime = await _context.Animes.FindAsync(animeId);
        var user = await _context.Users.FindAsync(userId);

        if (anime == null || user == null)
        {
            return NotFound("Anime or User not found.");
        }
        //verificar si user te una puntuació
        var existingAnimeScore = await _context.AnimeScores
        .FirstOrDefaultAsync(asc => asc.AnimeId == animeId && asc.UserId == userId);

        if (existingAnimeScore != null)
        {
            return BadRequest("User already added to this anime.");
        }

        var newAnimeScore = new AnimeScore
        {
            AnimeId = animeId,
            UserId = userId,
            Score = score
        };

        _context.AnimeScores.Add(newAnimeScore);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAnimeById), new { id = animeId }, newAnimeScore);
    }


    // PUT: api/Anime/{animeId}/User/{userId}
    [HttpPut("{animeId}/User/{userId}")]
    public async Task<ActionResult> UpdateUserAnime(int animeId, int userId, [FromBody] int score)
    {
        var existingAnimeScore = await _context.AnimeScores
            .FirstOrDefaultAsync(asc => asc.AnimeId == animeId && asc.UserId == userId);

        if (existingAnimeScore == null)
        {
            return NotFound("Anime-user association not found.");
        }

        existingAnimeScore.Score = score;

        _context.Entry(existingAnimeScore).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
