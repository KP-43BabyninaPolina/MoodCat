namespace TelegramBot.Bot.Services;

public class StatisticsSwitch
{
     private static bool isOn = false;

    public static void Switch()
    {
        isOn = !isOn;

    }

    public static bool IsOn() => isOn;

}
