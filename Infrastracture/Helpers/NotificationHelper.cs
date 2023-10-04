namespace Infrastracture.Helpers;

public class NotificationHelper
{


    public static string  GeneratePhoneCode()
    {
        Random rnd = new Random();
        int min = 100000;
        int max = 999999;
        int order = rnd.Next(min, max);
        //order = 123456;
        return order.ToString();
    }
}