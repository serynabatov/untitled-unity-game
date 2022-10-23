using System;
using UnityEngine;
public class Utilities
{
    public static string GetTimestamp(DateTime value)
    {
        return value.ToString("dddd, dd MMMM yyyy HH:mm:ss");
    }

    public static string NamingFile(string value, string pattern1, string pattern2)
    {
        return DateTime.ParseExact(value, pattern1, System.Globalization.CultureInfo.CurrentCulture).ToString(pattern2);
    }

}