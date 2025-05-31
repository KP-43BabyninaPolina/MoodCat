using System;
using Microsoft.Data.Sqlite;

namespace TelegramBot.Data.Services;
public class ExtractedContent
{
    public string Name { get; set; } = "";
    public string Link { get; set; } = "";
}
public class ContentService(SqliteConnection contentDB)
{
    private readonly SqliteConnection _db = contentDB;


    public async Task<ExtractedContent> GetContentAsync(string contentType, string currUserMood)
    {
        var query = $"SELECT {currUserMood} FROM {contentType}";

       await  using var command = new SqliteCommand(query, _db);
      await   using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var contentEntry = reader.GetString(0); // або відповідний тип і індекс
            string[] properties = contentEntry.Split('|');

            ExtractedContent content = new() { Name = properties[0], Link = properties[1] };
            return content;
        }
        return null;
    }
}
