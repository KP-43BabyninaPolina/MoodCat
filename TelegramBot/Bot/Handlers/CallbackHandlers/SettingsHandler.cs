using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Data;
using TelegramBot.Bot.Lib.Keyboards;

namespace TelegramBot.Bot.Handlers.CallbackHandlers;

public class SettingsHandler : ICallbackHandler
{
    public bool CanHandle(string data) => data == "B";

    public async Task HandleAsync(ITelegramBotClient bot, CallbackQuery query, string currUserMood, AppDbContext context, CancellationToken cancellationToken)
    {
        await bot.SendMessage(
            query.Message.Chat.Id,
            "Оберіть опцію:",
            replyMarkup: Keyboard.Settings,
            cancellationToken: cancellationToken);
    }
}