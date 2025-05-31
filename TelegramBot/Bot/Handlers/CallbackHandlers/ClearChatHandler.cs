using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Bot.Handlers.CallbackHandlers.TelegramBot.Bot.Handlers.ICallbackHandlers;
using TelegramBot.Bot.Lib.Keyboards;
using TelegramBot.Bot.Services;
using TelegramBot.Data;

namespace TelegramBot.Bot.Handlers.CallbackHandlers;

 public class ClearChatHandler : ICallbackHandler
{
     public bool CanHandle(string data) => data == "G";

   public async Task HandleAsync(
    ITelegramBotClient bot,
    CallbackQuery callbackQuery,
    string currUserMood,
    Dictionary<long, int> userLastMessageIds,
    AppDbContext context,
    CancellationToken cancellationToken)
{
    var chatId = callbackQuery.Message.Chat.Id;

    if (!HistorySwitch.IsOn())
    {
        await bot.SendMessage(
            chatId,
            "Очищення чату наразі вимкнено!",
            replyMarkup: Keyboard.MainMenu,
            cancellationToken: cancellationToken);
        return;
    }

    for (int i = 0; i < 100; i++)
    {
        try
        {
            int messageIdToDelete = callbackQuery.Message.MessageId - i;
            await bot.DeleteMessageAsync(chatId, messageIdToDelete, cancellationToken);
            Console.WriteLine($"[DEBUG] Видалено повідомлення з ID: {messageIdToDelete}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Не вдалося видалити повідомлення: {ex.Message}");
        }
    }

    var sentMessage = await bot.SendMessage(
        chatId,
        "Чат очищено! Почнемо з чистого аркуша 🌸",
        replyMarkup: Keyboard.MainMenu,
        cancellationToken: cancellationToken);

    userLastMessageIds[chatId] = sentMessage.MessageId;
}
}
