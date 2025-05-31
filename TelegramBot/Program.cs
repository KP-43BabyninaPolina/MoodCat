using System.Text;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Bot.Lib.Keyboards;
using TelegramBot.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TelegramBot.Services;
using TelegramBot.Bot.Services;
using TelegramBot.Bot.Lib.Methods;

namespace TelegramBot
{
    class Program
    {
        private static string Token { get; set; } = "7685257153:AAE77imIaHX-T5EyBlCKd8G_H71QI9hAKLA";
        private static TelegramBotClient? botClient;
        private static CommandRouter? commandRouter;
        private static string currUserMood = "";
        private static Dictionary<long, int> userLastMessageIds = new();

        static async Task Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            botClient = new TelegramBotClient(Token);
            commandRouter = new CommandRouter();

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("Default")));
            var contextFactory = new AppDbContextFactory();
            var context = contextFactory.CreateDbContext(Array.Empty<string>());
            await context.Database.EnsureCreatedAsync();

            using var cts = new CancellationTokenSource();

            var me = await botClient.GetMeAsync();
            Console.WriteLine($"@{me.Username} запущений... Натисніть Enter, щоб зупинити.");

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>(),
                DropPendingUpdates = true
            };

            botClient.StartReceiving(
                async (bot, update, cancellationToken) => await UpdateHandler(bot, update, context, cancellationToken), // ← передаємо context
                ErrorHandler,
                receiverOptions,
                cts.Token
            );

            Console.ReadLine();
            cts.Cancel();
        }

        private static Task ErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Помилка: {exception.Message}");
            return Task.CompletedTask;
        }

        // ← тепер context передається сюди!
        private static async Task UpdateHandler(
            ITelegramBotClient bot,
            Update update,
            AppDbContext context,
            CancellationToken cancellationToken)
        {
            if (update.Message is { Text: not null } message)
            {
                if (message.Text == "/start")
                {
                    var user = message.From;

                    UserService service = new(context);

                    await service.RegisterUserAsync(user!.Id, user!.FirstName);


                    await BotUtils.SendMessageReplacingOldAsync(
                      bot,
                      message.Chat.Id,
                      "Привіт! Я MoodCat, твій пухнастий помічник у світі настроїв! Обери, що тобі потрібно:",
                      Keyboard.MainMenu,
                      userLastMessageIds,
                      cancellationToken
                  );
                }
                else
                {
                    await BotUtils.SendMessageReplacingOldAsync(
                        bot,
                        message.Chat.Id,
                        "Мур! Для початку роботи надішли /start",
                        null,
                        userLastMessageIds,
                        cancellationToken
                    );
                }
            }
            else if (update.CallbackQuery is { Message: not null } callbackQuery)
            {
                var handler = commandRouter?.Route(callbackQuery.Data!);

                if (handler != null)
                {
                    await handler.HandleAsync(
                        bot,
                        callbackQuery,
                        currUserMood,
                        userLastMessageIds,
                        context,
                        cancellationToken);
                }
                else
                {
                    await BotUtils.SendMessageReplacingOldAsync(
                        bot,
                        callbackQuery.Message.Chat.Id,
                        "Ой-ой! Я не знаю, як це обробити.",
                        null,
                        userLastMessageIds,
                        cancellationToken
                    );
                }
            }
        }
    }
}