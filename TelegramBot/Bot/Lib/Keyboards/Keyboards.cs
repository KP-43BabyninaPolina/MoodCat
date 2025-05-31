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

    public static InlineKeyboardMarkup ContentKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Фільми", "MC") },
                    new[] { InlineKeyboardButton.WithCallbackData("Аніме", "AC") },
                    new[] { InlineKeyboardButton.WithCallbackData("Фото", "PC") }
                });

                 public static InlineKeyboardMarkup Settings = new InlineKeyboardMarkup(new[]
                {
                    new[] { InlineKeyboardButton.WithCallbackData("Ввімк/Вимк Очищення чату", "СS") },
                    new[] { InlineKeyboardButton.WithCallbackData("Ввімк/Вимк Статистику", "SS") },
                });
}