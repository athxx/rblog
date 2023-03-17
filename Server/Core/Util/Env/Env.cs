using System.Collections;

namespace Core.Util;

public class Env
{
    public static Hashtable Load()
    {
        // Load the .env file
        DotNetEnv.Env.Load();

        // Create a Hashtable to store environment variables
        var envVars = new Hashtable();

        // Get all environment variables
        foreach (var key in System.Environment.GetEnvironmentVariables().Keys)
        {
            envVars.Add(key, System.Environment.GetEnvironmentVariable(key.ToString()));
        }

        return envVars;
    }
}