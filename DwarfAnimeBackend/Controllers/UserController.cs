using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DwarfCodeData.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.Data;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/User
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users
            .Include(u => u.AnimeScores)
            .ThenInclude(au => au.Anime)
            .ToListAsync();
        return Ok(users);
    }

    // GET: api/User/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users
            .Include(u => u.AnimeScores)
            .ThenInclude(au => au.Anime)
            .FirstOrDefaultAsync(u => u.UserId == id);

        if (user == null)
            return NotFound(new { Message = "User not found" });

        return Ok(user);
    }

    // POST: api/User
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (user == null)
            return BadRequest(new { Message = "User data is invalid" });

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
    }

    // PUT: api/User/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
    {
        if (id != user.UserId)
            return BadRequest(new { Message = "User ID mismatch" });

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Users.Any(u => u.UserId == id))
                return NotFound(new { Message = "User not found" });

            throw;
        }

        return NoContent();
    }

    // DELETE: api/User/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound(new { Message = "User not found" });

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // POST: api/User/{userId}/Anime/{animeId}
    [HttpPost("{userId}/Anime/{animeId}")]
    public async Task<IActionResult> AddAnimeToUser(int userId, int animeId, [FromBody] int score)
    {
        if (score <= 0)
        {
            return BadRequest(new { Message = "Invalid score. The score must be greater than 0." });
        }

        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return NotFound(new { Message = "User not found." });
        }

        var anime = await _context.Animes.FindAsync(animeId);
        if (anime == null)
        {
            return NotFound(new { Message = "Anime not found." });
        }

        // Verifiquem si la relació existex
        var existingAnimeScore = await _context.AnimeScores
            .FirstOrDefaultAsync(asc => asc.UserId == userId && asc.AnimeId == animeId);

        if (existingAnimeScore != null)
        {
            return BadRequest(new { Message = "User already has a score for this anime." });
        }

        //relació animeScores
        var animeScore = new AnimeScore
        {
            UserId = userId,
            AnimeId = animeId,
            Score = score 
        };

        _context.AnimeScores.Add(animeScore);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = userId }, animeScore);
    }

    // PUT: api/User/{userId}/Anime/{animeId}
    [HttpPut("{userId}/Anime/{animeId}")]
    public async Task<IActionResult> UpdateAnimeForUser(int userId, int animeId, [FromBody] AnimeScore animeScore)
    {
        var existingAnimeScore = await _context.AnimeScores
            .FirstOrDefaultAsync(asc => asc.UserId == userId && asc.AnimeId == animeId);

        if (existingAnimeScore == null)
            return NotFound(new { Message = "Anime-score association not found" });

        // Actualizamos el score
        existingAnimeScore.Score = animeScore.Score;

        _context.Entry(existingAnimeScore).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/User/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestVerification loginRequest)
    {
        if (loginRequest == null || string.IsNullOrEmpty(loginRequest.EmailOrAlias) || string.IsNullOrEmpty(loginRequest.Password))
        {
            return BadRequest(new { Message = "Email/Password cannot be empty" });
        }

        // Buscar el usuario por correo o alias (Name)
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Mail == loginRequest.EmailOrAlias || u.Name == loginRequest.EmailOrAlias);  // Usamos un OR para buscar por Mail o Name

        if (user == null)
        {
            return NotFound(new { Message = "User not found" });
        }

        // Verificar la contraseña
        if (user.Pasword != loginRequest.Password)  // Usamos la propiedad Pasword para la comparación
        {
            return Unauthorized(new { Message = "Incorrect password" });
        }

        // Si las credenciales son correctas
        return Ok(new { Message = "Login successful", UserId = user.UserId });
    }

    // GET: api/User/{userId}/AvailableAnimes
    [HttpGet("{userId}/AvailableAnimes")]
    public async Task<IActionResult> GetAvailableAnimes(int userId)
    {
    var user = await _context.Users
        .Include(u => u.AnimeScores)
        .ThenInclude(au => au.Anime)
        .FirstOrDefaultAsync(u => u.UserId == userId);
    
    if (user == null)
    {
        return NotFound(new { Message = "User not found" });
    }
    
    // Obtener los animes que no están en la lista del usuario
    var availableAnimes = await _context.Animes
        .Where(a => !_context.AnimeScores
            .Any(aus => aus.UserId == userId && aus.AnimeId == a.AnimeId))
        .ToListAsync();
    
    return Ok(availableAnimes);
    }

}
