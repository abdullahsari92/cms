namespace AS.Core.Constants;

public static class EmailConstants
{
    public static string EmailKeyRegEx => @"\{\%[<" + EmailFrom + @">]*\%\}";
    public static string EmailFrom => "bid";
}
