using System;
public class Utilities
{
    public static string GetTimestamp(DateTime value)
    {
        return value.ToString("yyyy-MM-dd-HH-mm-ss-ffff");
    }
}