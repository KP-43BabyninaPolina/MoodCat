using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Bot.Handlers.CallbackHandlers.TelegramBot.Bot.Handlers.ICallbackHandlers;
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
                var moodKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Веселий", "HO") },
                    new[] { InlineKeyboardButton.WithCallbackData("Сумний", "SO") },
                    new[] { InlineKeyboardButton.WithCallbackData("Злий", "AO") },
                    new[] { InlineKeyboardButton.WithCallbackData("Виснажений", "TO") },
                    new[] { InlineKeyboardButton.WithCallbackData("Спокійний", "CO") }
                });

                await bot.SendMessage(
                    chatId,
                    "Обери свій кото-настрій на сьогодні! 🐾",
                    replyMarkup: moodKeyboard,
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
                    replyMarkup: contentKeyboard,
                    cancellationToken: cancellationToken
                );

                break;
        }
    }
}