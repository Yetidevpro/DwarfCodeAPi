
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
        var animes = _context.Animes.ToList();
        return Ok(animes);
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

        // Devolvemos el recurso creado con código 201 Created
        return StatusCode(201, anime);
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

}
