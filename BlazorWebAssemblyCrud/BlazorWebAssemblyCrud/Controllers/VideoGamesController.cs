using BlazorWebAssemblyCrud.Data;
using Microsoft.AspNetCore.Mvc;
using BlazorWebAssembly.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebAssemblyCrud.Controllers;

[ApiController]
[Route("api/videogames")]
public class VideoGamesController: ControllerBase
{
    private readonly DataContext _context;

    public VideoGamesController(DataContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<VideoGame>>> GetAllVideoGames()
    {
        return await _context.VideoGames.ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<VideoGame>> GetVideoGameById(int id)
    {
        var videoGame = await _context.VideoGames.FindAsync(id);
        if (videoGame == null)
        {
            return NotFound("Game not found");
        }
        return Ok(videoGame);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<VideoGame> DeleteVideoGame(int id)
    {
        var videoGame = await _context.VideoGames.FindAsync(id);
        if (videoGame == null)
        {
            return NotFound("Game not found");
        }
        
        _context.Remove(videoGame);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<VideoGame>> UpdateVideoGame(int id, VideoGame videoGame)
    {
        if (id != videoGame.Id)
        {
            return BadRequest("Id mismatch");
        }
        
        var dbGame = await _context.VideoGames.FindAsync(id);
        if (dbGame == null)
        {
            return NotFound("Game not found");
        }
        
        _context.Entry(videoGame).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();
        
        return Ok(videoGame);
    }

    [HttpPost]
    public async Task<ActionResult<VideoGame>> CreateVideoGame(VideoGame videoGame)
    {
        _context.Add(videoGame);
        await _context.SaveChangesAsync();
        
        return Ok(videoGame);
    }
}