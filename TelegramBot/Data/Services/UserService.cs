using Microsoft.EntityFrameworkCore;
using TelegramBot.Data;

namespace TelegramBot.Services;

using Data.Models;

public class UserService
{
    private readonly AppDbContext _db;
    

    public UserService(AppDbContext db) => _db = db;

    public async Task RegisterUserAsync(long tgId, string? username)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.TelegramId == tgId);
        if (user is null)
        {
            _db.Users.Add(new Person(){ TelegramId = tgId, Username = username ?? "unknown" });
            await _db.SaveChangesAsync();
        }
    }
}
