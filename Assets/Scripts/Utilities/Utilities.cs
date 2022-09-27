using System;
public class Utilities
{
    public static string GetTimestamp(DateTime value)
    {
        return value.ToString("dddd, dd MMMM yyyy HH:mm:ss");
    }
}