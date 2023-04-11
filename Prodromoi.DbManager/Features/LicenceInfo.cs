namespace Prodromoi.DbManager.Features;

public static class LicenceInfo
{
    public static string LicenseInfo = $"Prodromoi Copyright (C) 2023 Owen Holloway (lanceolata.com.au)\n" +
                                       $"This program comes with ABSOLUTELY NO WARRANTY;\n" +
                                       $"This is free software, and you are welcome to redistribute it\n"+
                                       $"under certain conditions;\n\n";

    public static void OutputLicense()
    {
        Console.Write(LicenseInfo);
    }
    
}