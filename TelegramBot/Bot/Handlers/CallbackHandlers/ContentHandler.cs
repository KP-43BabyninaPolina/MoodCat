using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Bot.Handlers.CallbackHandlers.TelegramBot.Bot.Handlers.ICallbackHandlers;
using TelegramBot.Bot.Lib.Keyboards;
using TelegramBot.Bot.Lib.Methods;
using TelegramBot.Bot.Services;
using TelegramBot.Data;

namespace TelegramBot.Bot.Handlers.CallbackHandlers;

public class ContentHandler : ICallbackHandler
{
    public bool CanHandle(string data) => new[] { "MC", "AC", "PC", "E" }.Contains(data);

    public async Task HandleAsync(
        ITelegramBotClient bot,
        CallbackQuery query,
        string currUserMood,
        Dictionary<long, int> userLastMessageIds,
        AppDbContext context, // ← ОБОВ’ЯЗКОВО
        CancellationToken cancellationToken)
    {
        var userId = query.From.Id;

        string contentCode = query.Data;
        if (contentCode != "E")
        {
            switch (query.Data)
            {
                case "MC":
                    if (CurrentContentManager.GetType(userId) != null)
                    {
                        CurrentContentManager.ClearType(userId);
                    }
                    CurrentContentManager.SetType(userId, "movies");
                    break;

                case "AC":
                    if (CurrentContentManager.GetType(userId) != null)
                    {
                        CurrentContentManager.ClearType(userId);
                    }
                    CurrentContentManager.SetType(userId, "anime");
                    break;


                case "PC":
                    if (CurrentContentManager.GetType(userId) != null)
                    {
                        CurrentContentManager.ClearType(userId);
                    }
                    CurrentContentManager.SetType(userId, "photos");
                    break;
            };
        }

        string contentType = CurrentContentManager.GetType(userId);
        
        if (!string.IsNullOrEmpty(contentType))
        {
            currUserMood = CurrentMoodManager.GetMood(userId);
            if (currUserMood != null)
            {
                await BotMethod.GenerateContent(bot, query.Message.Chat.Id, contentType, currUserMood, cancellationToken);

                await BotMethod.AskNextAsync(bot, query.Message.Chat.Id, cancellationToken);
            }
            else
            {
                var chatId = query.Message.Chat.Id;

                await bot.SendMessage(
                    chatId, "Настрій ще не обрано: будь-ласка, спершу оберіть його в меню.",
                    replyMarkup: Keyboard.Mood,
                    cancellationToken: cancellationToken);
            }
        }
        else
        {
            var chatId = query.Message.Chat.Id;

            await bot.SendMessage(
                chatId, "Тип контенту ще не обрано: будь-ласка, спершу оберіть його в меню.",
                replyMarkup: Keyboard.Content,
                cancellationToken: cancellationToken);
        }
    }
}

