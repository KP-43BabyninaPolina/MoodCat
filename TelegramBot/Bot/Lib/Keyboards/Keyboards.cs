using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Bot.Lib.Keyboards;

public static class Keyboard
{
    public static InlineKeyboardMarkup MainMenu = new InlineKeyboardMarkup(new[]
    {
        new[] { InlineKeyboardButton.WithCallbackData("Обрати настрій", "C") },
        new[] { InlineKeyboardButton.WithCallbackData("Налаштування", "B") },
        new[] { InlineKeyboardButton.WithCallbackData("Статистика настрою", "A") }
    });

    public static InlineKeyboardMarkup Content = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Фільми", "MC") },
                    new[] { InlineKeyboardButton.WithCallbackData("Аніме", "AC") },
                    new[] { InlineKeyboardButton.WithCallbackData("Фото", "PC") }
                });

    public static InlineKeyboardMarkup Mood = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Веселий", "HO") },
                    new[] { InlineKeyboardButton.WithCallbackData("Сумний", "SO") },
                    new[] { InlineKeyboardButton.WithCallbackData("Злий", "AO") },
                    new[] { InlineKeyboardButton.WithCallbackData("Виснажений", "TO") },
                    new[] { InlineKeyboardButton.WithCallbackData("Спокійний", "CO") }
                });

    public static InlineKeyboardMarkup Settings = new InlineKeyboardMarkup(new[]
  {
                    new[] { InlineKeyboardButton.WithCallbackData("Ввімк/Вимк Очищення чату", "СS") },
                    new[] { InlineKeyboardButton.WithCallbackData("Ввімк/Вимк Статистику", "SS") },
                });
}