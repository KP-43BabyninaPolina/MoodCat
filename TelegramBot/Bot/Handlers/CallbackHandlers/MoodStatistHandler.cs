using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Data;
using TelegramBot.Bot.Lib.Methods;
using Microsoft.AspNetCore.Routing.Constraints;
using TelegramBot.Bot.Lib.Keyboards;
using TelegramBot.Bot.Services;


namespace TelegramBot.Bot.Handlers.CallbackHandlers;

public class MoodStatistHandler : ICallbackHandler
{
    public bool CanHandle(string data) => data == "A";

    public async Task HandleAsync(ITelegramBotClient bot, CallbackQuery query, string currUserMood, AppDbContext context, CancellationToken cancellationToken)
    {
        long tgId = query.From.Id;
        long chatId = query.Message.Chat.Id;
        
        if (!StatisticsSwitch.IsOn())
        {
            await bot.SendMessage(
                chatId,
                "Функція збору статистики вимкнена! Щоб переглянути, спершу ввімкніть її у налаштуваннях.",
                cancellationToken: cancellationToken);
        }
        else
        {
            await BotMethod.ViewStatistics(tgId, context, bot, chatId, cancellationToken);
        }

        await bot.SendMessage(chatId, "Ти у головному меню:", replyMarkup: Keyboard.MainMenu, cancellationToken: cancellationToken);
    }
}
