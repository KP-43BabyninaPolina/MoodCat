using System;

namespace TelegramBot.Bot.Services;

public class CurrentContentManager
{
            private static readonly Dictionary<long, string> UserContTypes = new();

        /// <summary>
        /// Зберігає або оновлює настрій користувача
        /// </summary>
        public static void SetType(long userId, string type)
        {
            UserContTypes[userId] = type;
        }

        /// <summary>
        /// Повертає настрій користувача, якщо такий є
        /// </summary>
        public static string? GetType(long userId)
        {
            return UserContTypes.TryGetValue(userId, out var type) ? type : null;
        }

        /// <summary>
        /// Видаляє настрій (наприклад, після завершення сесії)
        /// </summary>
        public static void ClearType(long userId)
        {
            UserContTypes.Remove(userId);
        }

}
