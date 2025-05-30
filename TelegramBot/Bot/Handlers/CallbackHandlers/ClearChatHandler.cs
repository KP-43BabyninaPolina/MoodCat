using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Bot.Handlers.CallbackHandlers.TelegramBot.Bot.Handlers.ICallbackHandlers;
using TelegramBot.Bot.Lib.Keyboards;
using TelegramBot.Data;

namespace TelegramBot.Bot.Handlers.CallbackHandlers;

 public class ClearChatHandler : ICallbackHandler
{
     public bool CanHandle(string data) => data == "G";

    public async Task HandleAsync(
        ITelegramBotClient bot,
        CallbackQuery callbackQuery,
        Dictionary<long, string> userMoods,
        Dictionary<long, int> userLastMessageIds,
        AppDbContext context, // ← ОБОВ’ЯЗКОВО
        CancellationToken cancellationToken)
    {
        var chatId = callbackQuery.Message.Chat.Id;

        // Видаляємо останні 5 повідомлень (включно з повідомленням кнопки)
        for (int i = 0; i < 5; i++)
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

        // Очищення настрою
        userMoods.Remove(chatId);

        // Нове повідомлення
        var sentMessage = await bot.SendMessage(
            chatId,
            "Чат очищено! Почнемо з чистого аркуша 🌸",
            replyMarkup: Keyboard.MainMenu,
            cancellationToken: cancellationToken);

        userLastMessageIds[chatId] = sentMessage.MessageId;
    }
}
