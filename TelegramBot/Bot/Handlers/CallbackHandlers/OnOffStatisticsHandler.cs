using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Data;
using TelegramBot.Bot.Lib.Keyboards;
using TelegramBot.Bot.Lib.Methods;
using TelegramBot.Bot.Services;





namespace TelegramBot.Bot.Handlers.CallbackHandlers;

public class OnOffStatisticsHandler : ICallbackHandler
{
    public bool CanHandle(string data) => data == "SS";
  public async Task HandleAsync(ITelegramBotClient bot, CallbackQuery query, string currUserMood, AppDbContext context, CancellationToken cancellationToken)
    {
        BotMethod.SwitchStatistics();

        string message = "Функція статистики ";
        if (StatisticsSwitch.IsOn())
        {
            message += "ввімкнена!";
        }
        else
        {
            message += "вимкнена!";
        }

        await bot.SendMessage(
                    query.Message.Chat.Id,
                    message,
                    replyMarkup: Keyboard.MainMenu,
                    cancellationToken: cancellationToken);
    }
}
