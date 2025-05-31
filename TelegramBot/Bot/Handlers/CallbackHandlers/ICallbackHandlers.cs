using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Data;

namespace TelegramBot.Bot.Handlers.CallbackHandlers
{
    namespace TelegramBot.Bot.Handlers.ICallbackHandlers
{
    public interface ICallbackHandler
    {
        bool CanHandle(string data);

            Task HandleAsync(
         ITelegramBotClient bot,
         CallbackQuery callbackQuery,
         string currUserMood,
        Dictionary<long, int> userLastMessageIds,
        AppDbContext context, // ← ДОДАЛИ ЦЕ
        CancellationToken cancellationToken
    );

    }
}

}
