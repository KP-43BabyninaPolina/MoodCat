using Microsoft.EntityFrameworkCore;
using TelegramBot.Data;

namespace TelegramBot.Services;

public class MoodService
{
        private readonly AppDbContext _db;


    public MoodService(AppDbContext db) => _db = db;

    public async Task UpdateMoodCounterAsync(long tgId, string currUserMood)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.TelegramId == tgId);

        Console.WriteLine($"Happy: {user.HappyCounter}, Sad: {user.SadCounter}");


        if (user is not null && currUserMood != null)
        {
            switch (currUserMood)
            {
                case "Happy":
                    user.HappyCounter += 1;
                    break;

                case "Sad":
                    user.SadCounter += 1;
                    break;

                case "Angry":
                    user.AngryCounter += 1;
                    break;

                case "Tired":
                    user.TiredCounter += 1;
                    break;

                case "Calm":
                    user.CalmCounter += 1;
                    break;

            }
             _db.SaveChanges();

        var test = await _db.Users.FirstOrDefaultAsync(u => u.TelegramId == tgId);
Console.WriteLine($"Після збереження: Happy = {test.HappyCounter}");

        }
        else
        {
            System.Console.WriteLine("Зміни не збережено.");
        }
        
    }
}