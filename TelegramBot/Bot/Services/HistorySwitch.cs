using System;

namespace TelegramBot.Bot.Services;

public class HistorySwitch
{
    private static bool isOn = false;

    public static void Switch() => isOn = !isOn;

    public static bool IsOn() => isOn;

}
