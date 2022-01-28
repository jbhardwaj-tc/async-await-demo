namespace AsyncAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SongsController : ControllerBase
{
    private readonly ILogger _logger;

    public SongsController(ILogger<SongsController> logger) => _logger = logger;

    [HttpGet("youtube-music")]
    public async Task<IActionResult> GetFromYoutubeMusic(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting getting songs from youtube");
        
        await Task.Delay(5000, cancellationToken);

        var song = new SongDto("Sign of the times", "Harry Styles", "Album 1");

        _logger.LogInformation("Fetched song successfully");
        
        return Ok(song);
    }

    [HttpGet("spotify")]
    public async Task<IActionResult> GetFromSpotify()
    {
        await Task.Delay(1000);
        return Ok(new SongDto("Dangerous", "David Guetta", "Album 12"));
    }

    [HttpGet("apple-music")]
    public async Task<IActionResult> GetFromAppleMusic()
    {
        await Task.Delay(1000);
        return Ok(new SongDto("Leave a light on", "Tom Walker", "Album 1"));
    }
}

public record SongDto(string Title, string ArtistName, string AlbumName);