using System;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Bot.Lib.Methods;

public static class BotUtils
{
    public static async Task SendMessageReplacingOldAsync(
        ITelegramBotClient bot,
        long chatId,
        string text,
        ReplyMarkup? replyMarkup,
        Dictionary<long, int> userLastMessageIds,
        CancellationToken cancellationToken)
    {
        if (userLastMessageIds.TryGetValue(chatId, out int lastMessageId))
        {
            try
            {
                await bot.DeleteMessageAsync(chatId, lastMessageId, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[!] Delete failed: {ex.Message}");
            }
        }

        var msg = await bot.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup, cancellationToken: cancellationToken);
        userLastMessageIds[chatId] = msg.MessageId;
    }
}
