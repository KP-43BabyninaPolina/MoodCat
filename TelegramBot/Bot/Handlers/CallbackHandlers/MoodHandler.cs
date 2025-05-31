using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Bot.Handlers.CallbackHandlers.TelegramBot.Bot.Handlers.ICallbackHandlers;
using TelegramBot.Bot.Lib.Keyboards;
using TelegramBot.Bot.Services;
using TelegramBot.Data;
using TelegramBot.Services;


namespace TelegramBot.Bot.Handlers.CallbackHandlers;

public class MoodHandler : ICallbackHandler
{
    private static readonly HashSet<string> MoodCodes = new() { "HO", "SO", "AO", "TO", "CO", "C" };

    public bool CanHandle(string data) => MoodCodes.Contains(data);


    public async Task HandleAsync(
    ITelegramBotClient bot,
        CallbackQuery callbackQuery,
        string currUserMood,
        Dictionary<long, int> userLastMessageIds,
        AppDbContext context, // ← ОБОВ’ЯЗКОВО
        CancellationToken cancellationToken)
    {
        var chatId = callbackQuery.Message.Chat.Id;
        var data = callbackQuery.Data;

        switch (data)
        {
            case "C":
    

                await bot.SendMessage(
                    chatId,
                    "Обери свій кото-настрій на сьогодні! 🐾",
                    replyMarkup: Keyboard.Mood,
                    cancellationToken: cancellationToken
                );
                break;

            case "HO":
            case "SO":
            case "AO":
            case "TO":
            case "CO":
                currUserMood = data;
                var userId = callbackQuery.From.Id;

                if (CurrentMoodManager.GetMood(userId) != null)
                {
                    CurrentMoodManager.ClearMood(userId);
                }
                
                CurrentMoodManager.SetMood(userId, data);

                if (StatisticsSwitch.IsOn())
                {
                    MoodService service = new(context);
                    await service.UpdateMoodCounterAsync(userId, currUserMood);
                }
                
                var contentKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Фільми", "MC") },
                    new[] { InlineKeyboardButton.WithCallbackData("Аніме", "AC") },
                    new[] { InlineKeyboardButton.WithCallbackData("Фото", "PC") }
                });

                await bot.SendMessage(
                    chatId,
                    "Ваш настрій зафіксовано! Що бажаєте переглянути?",
                    replyMarkup: Keyboard.Content,
                    cancellationToken: cancellationToken
                );

                break;
        }
    }
}