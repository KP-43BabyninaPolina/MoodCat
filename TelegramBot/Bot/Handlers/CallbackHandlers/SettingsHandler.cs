using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Bot.Handlers.CallbackHandlers;
using TelegramBot.Bot.Handlers.CallbackHandlers.TelegramBot.Bot.Handlers.ICallbackHandlers;
using TelegramBot.Data;

namespace TelegramBot.Bot.Handlers.CallbackHandlers;

 public class SettingsHandler : ICallbackHandler
    {
        public bool CanHandle(string data) => data == "B";

        public async Task HandleAsync(
         ITelegramBotClient bot,
        CallbackQuery callbackQuery,
        Dictionary<long, string> userMoods,
        Dictionary<long, int> userLastMessageIds,
        AppDbContext context, // ← ОБОВ’ЯЗКОВО
        CancellationToken cancellationToken)
        {
            await bot.SendMessage(
                callbackQuery.Message.Chat.Id,
                "Налаштування ще в розробці :)",
                cancellationToken: cancellationToken);
        }
    }