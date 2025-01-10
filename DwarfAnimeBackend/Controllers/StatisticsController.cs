using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DwarfCodeData.Models;  


[Route("api/[controller]")]
[ApiController]
public class StatisticsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StatisticsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Statistics/TopUsers
    [HttpGet("TopUsers")]
    public ActionResult GetTopUsersWithMostAnimes()
    {
        var topUsers = _context.AnimeScores
            .GroupBy(au => au.UserId)
            .Select(group => new
            {
                UserId = group.Key,
                AnimeCount = group.Count()
            })
            .OrderByDescending(x => x.AnimeCount)
            .Take(10)
            .Join(
                _context.Users,
                result => result.UserId,
                user => user.UserId,
                (result, user) => new { user.Name, result.AnimeCount }
            )
            .ToListAsync();

        return Ok(topUsers);
    }

    // GET: api/Statistics/TopAnimes
    [HttpGet("TopAnimes")]
    public ActionResult GetTopMostViewedAnimes()
    {
        var topAnimes = _context.AnimeScores
            .GroupBy(au => au.AnimeId)
            .Select(group => new
            {
                AnimeId = group.Key,
                UserCount = group.Count()
            })
            .OrderByDescending(x => x.UserCount)
            .Take(10)
            .Join(
                _context.Animes,
                result => result.AnimeId,
                anime => anime.AnimeId,
                (result, anime) => new { anime.Name, result.UserCount }
            )
            .ToList();

        return Ok(topAnimes);
    }

    // GET: api/Statistics/TopRated
    [HttpGet("TopRated")]
    public ActionResult GetTopBestRatedAnimes()
    {
        var topRatedAnimes = _context.AnimeScores
            .GroupBy(asc => asc.AnimeId)
            .Select(group => new
            {
                AnimeId = group.Key,
                AverageScore = group.Average(asc => asc.Score)
            })
            .OrderByDescending(x => x.AverageScore)
            .Take(10)
            .Join(
                _context.Animes,
                result => result.AnimeId,
                anime => anime.AnimeId,
                (result, anime) => new { anime.Name, result.AverageScore }
            )
            .ToList();

        return Ok(topRatedAnimes);
    }

}
