using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Bot.Handlers.CallbackHandlers.TelegramBot.Bot.Handlers.ICallbackHandlers;
using TelegramBot.Bot.Lib.Keyboards;
using TelegramBot.Bot.Lib.Methods;
using TelegramBot.Bot.Services;
using TelegramBot.Data;

namespace TelegramBot.Bot.Handlers.CallbackHandlers;

public class OnOffHistoryHandler : ICallbackHandler
{
    public bool CanHandle(string data) => data == "HS"; // HS – наприклад, код кнопки

    public async Task HandleAsync(
        ITelegramBotClient bot,
        CallbackQuery query,
        string currUserMood,
        Dictionary<long, int> userLastMessageIds,
        AppDbContext context,
        CancellationToken cancellationToken)
    {
        BotMethod.SwitchHistory();

        string message = "Очищення чату ";
        if (HistorySwitch.IsOn())
        {
            message += "ввімкнено!";
        }
        else
        {
            message += "вимкнено!";
        }

        await bot.SendMessage(
            query.Message.Chat.Id,
            message,
            replyMarkup: Keyboard.MainMenu,
            cancellationToken: cancellationToken);
    }
}