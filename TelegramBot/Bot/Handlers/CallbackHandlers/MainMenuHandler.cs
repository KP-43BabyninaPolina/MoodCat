using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Bot.Handlers.CallbackHandlers.TelegramBot.Bot.Handlers.ICallbackHandlers;
using TelegramBot.Bot.Lib.Keyboards;
using TelegramBot.Bot.Lib.Methods;
using TelegramBot.Data;

namespace TelegramBot.Bot.Handlers.CallbackHandlers;

public class MainMenuHandler : ICallbackHandler
{
    public bool CanHandle(string data) => data == "F"; // тільки F

    public async Task HandleAsync(
         ITelegramBotClient bot,
        CallbackQuery query,
        string currUserMood,
        Dictionary<long, int> userLastMessageIds,
        AppDbContext context, // ← ОБОВ’ЯЗКОВО
        CancellationToken cancellationToken)
    {
        var chatId = query.Message.Chat.Id;

        await BotUtils.SendMessageReplacingOldAsync(
            bot,
            chatId,
            "Ти у головному меню:",
            Keyboard.MainMenu,
            userLastMessageIds,
            cancellationToken
        );
    }
}
