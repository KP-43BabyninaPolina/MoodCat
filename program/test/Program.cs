﻿using System;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Test
{
    class Program
    {
        private static string Token {get; set;} = "7685257153:AAE77imIaHX-T5EyBlCKd8G_H71QI9hAKLA"; //private static readonly string Token = "7685257153:AAE77imIaHX-T5EyBlCKd8G_H71QI9hAKLA";
        private static TelegramBotClient botClient;

        static async Task Main()
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            botClient = new TelegramBotClient(Token);
            using var cts = new CancellationTokenSource();

            var me = await botClient.GetMeAsync();
            Console.WriteLine($"@{me.Username} Запущений... Натисніть Enter щоб зупинити.");

            var receiverOptions = new ReceiverOptions
            {
             AllowedUpdates = Array.Empty<UpdateType>(),
             DropPendingUpdates = true
            };

            botClient.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions, cts.Token);

            //client.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions, cts.Token);
            Console.ReadLine();
            cts.Cancel();
        }

       /* private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is { } message)
            {
                Console.WriteLine($"Отримано повідомлення: {message.Text}");
                await botClient.SendTextMessageAsync(message.Chat.Id, "Привіт! Це тестовий бот.", cancellationToken: cancellationToken);
            }
        }

        private static Task ErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Помилка: {exception.Message}");
            return Task.CompletedTask;
        }*/

         private static async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is { } message)
        {
            
            if (message.Text == "/start")
            {
                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    
                      new[]  {InlineKeyboardButton.WithCallbackData("А", "A"),},
                      new[]  {InlineKeyboardButton.WithCallbackData("Налаштування", "B")},
                      new[]  {InlineKeyboardButton.WithCallbackData("В", "C")}
                    
                });

                await bot.SendTextMessageAsync(message.Chat.Id, "Доброго дня! Виберіть опцію:", replyMarkup: keyboard, cancellationToken: cancellationToken);
            }
        }
        else if (update.CallbackQuery is { } callbackQuery)
    {
       /* string response = callbackQuery.Data switch
        {
            "A" => "Ви вибрали А! Це чудовий вибір!",
            "B" => "Варіант Б — це цікавий вибір!",
            "C" => "Ви натиснули В! Дякую за вибір!",
            _ => "Щось пішло не так..."
        };

        await bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, response, cancellationToken: cancellationToken);*/
        switch (update.CallbackQuery.Data)
            {
                case "B":
                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    
                    InlineKeyboardButton.WithCallbackData("Ввімкнути/вимкнути збір статистики", "BA"),
                    InlineKeyboardButton.WithCallbackData("Ввімкнути/вимкнути історію", "BB"),
                    
                });

                await bot.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Відкрито налаштування. Виберіть опцію:", replyMarkup: keyboard, cancellationToken: cancellationToken);
                break;

                case "BA":
                  await bot.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Статистика ввімкнута/вимкнута.", cancellationToken: cancellationToken);
                break;

                case "BB":
                 await bot.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Історія чату ввімкнута/вимкнута.", cancellationToken: cancellationToken);

                break;

                default:
                await bot.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Щось пішло не так...", cancellationToken: cancellationToken);
                break;
            }
            {
        };
    }
}
    private static Task ErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Помилка: {exception.Message}");
        return Task.CompletedTask;
    }

    }
}
