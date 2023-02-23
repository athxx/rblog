using Core.Util;

namespace Core.Config;

public static class Config
{
    public static string DbUser;
    public static string DbPsw;
    public static string DbName;
    public static string DbHost;
    public static string DbPort;

    static Config()
    {
        var cfg = Env.Load();
        DbHost = cfg["db_host"]?.ToString();
        DbPort = cfg["db_port"]?.ToString();
        DbName = cfg["db_name"]?.ToString();
        DbUser = cfg["db_user"]?.ToString();
        DbPsw = cfg["db_psw"]?.ToString();
    }
}