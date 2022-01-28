namespace AsyncAwaitClient;

class Program
{
    public static async Task Main(string[] args)
    {
        var client = new HttpClient();
        var watch = Stopwatch.StartNew();

        var youtubeSongTask = GetSongFromYoutube(client);
        var spotifySongTask = GetSongFromSpotify(client);
        var appleMusicSongTask = GetSongFromAppleMusic(client);

        Task<SongDto>[] tasks = { youtubeSongTask, spotifySongTask, appleMusicSongTask };
        try
        {
            await Task.WhenAll(youtubeSongTask, spotifySongTask, appleMusicSongTask);
            watch.Stop();
            Console.WriteLine($"Completed in {watch.ElapsedMilliseconds} ms");
        }
        catch (Exception)
        {
            var exceptions = tasks.Where(x => x.Exception is not null).Select(x => x.Exception);
            Console.WriteLine(exceptions.Select(x => $"{x.Message}{Environment.NewLine}"));
            Console.Read();
            return;
        }

        var allSongs = new List<SongDto> { youtubeSongTask.Result, spotifySongTask.Result, appleMusicSongTask.Result };


        Console.WriteLine(JsonSerializer.Serialize(allSongs, new JsonSerializerOptions { WriteIndented = true }));
        Console.Read();
    }

    private static async Task<SongDto> GetSongFromYoutube(HttpClient client)
    {
        // NOTE: Observer this cancellation timeout. This will be passed to the server.
        // 1.) Put a breakpoint on the song controller action method and observer the  value of
        // cancellationToken.IsCancellationRequested property.
        // 2.) Since the server action method has a delay of 5 seconds, it will be cancelled
        // before completion. 
        // 3.) Play around with the values and see difference in behaviour
        using var cancellationTokenSource = new CancellationTokenSource(4000);

        var response = await client.GetStringAsync("http://localhost:5266/api/Songs/youtube-music", cancellationTokenSource.Token);
        var song = JsonSerializer.Deserialize<SongDto>(response);

        return song;
    }

    private static async Task<SongDto> GetSongFromSpotify(HttpClient client)
    {
        var response = await client.GetStringAsync("http://localhost:5266/api/Songs/spotify");
        var song = JsonSerializer.Deserialize<SongDto>(response);

        return song;
    }

    private static async Task<SongDto> GetSongFromAppleMusic(HttpClient client)
    {
        var response = await client.GetStringAsync("http://localhost:5266/api/Songs/apple-music");
        var song = JsonSerializer.Deserialize<SongDto>(response);

        return song;
    }
}

public record SongDto(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("artistName")] string ArtistName,
    [property: JsonPropertyName("albumName")] string AlbumName);