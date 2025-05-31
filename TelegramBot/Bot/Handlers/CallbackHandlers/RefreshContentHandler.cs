using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Bot.Handlers.CallbackHandlers.TelegramBot.Bot.Handlers.ICallbackHandlers;
using TelegramBot.Bot.Lib.Keyboards;
using TelegramBot.Data;

namespace TelegramBot.Bot.Handlers.CallbackHandlers;

public class RefreshContentHandler : ICallbackHandler
{
    public bool CanHandle(string data) => data == "R";

    public async Task HandleAsync(ITelegramBotClient bot, CallbackQuery query, string currUserMood, Dictionary<long, int> userLastMessageIds, AppDbContext context, CancellationToken cancellationToken)
    {
         await bot.SendMessage(
                    query.Message.Chat.Id,
                    "Ваш настрій зафіксовано! Що бажаєте переглянути?",
                    replyMarkup: Keyboard.Content,
                    cancellationToken: cancellationToken
                );
    }
}
