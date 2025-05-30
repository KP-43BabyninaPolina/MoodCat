using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Bot.Handlers.CallbackHandlers.TelegramBot.Bot.Handlers.ICallbackHandlers;
using TelegramBot.Bot.Lib.Methods;
using TelegramBot.Data;

namespace TelegramBot.Bot.Handlers.CallbackHandlers;

public class ContentHandler : ICallbackHandler
{
    public bool CanHandle(string data) => new[] { "MC", "AC", "PC" }.Contains(data);

    public async Task HandleAsync(
        ITelegramBotClient bot,
        CallbackQuery callbackQuery,
        Dictionary<long, string> userMoods,
        Dictionary<long, int> userLastMessageIds,
        AppDbContext context, // ← ОБОВ’ЯЗКОВО
        CancellationToken cancellationToken)
    {
        string contentType = callbackQuery.Data switch
        {
            "MC" => "movies",
            "AC" => "anime",
            "PC" => "photos",
            _ => ""
        };

        if (!string.IsNullOrEmpty(contentType))
        {
            var chatId = callbackQuery.Message.Chat.Id;
            await BotMethod.GenerateContent(bot, chatId, contentType, userMoods, cancellationToken);
            await BotMethod.AskNextAsync(bot, chatId, cancellationToken);
        }
    }
}

