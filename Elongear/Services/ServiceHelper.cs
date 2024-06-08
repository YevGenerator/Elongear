namespace Elongear.Services;

public static class ServiceHelper
{
    public static IServiceProvider Provider { get; set; }

    public static T GetService<T>() where T: notnull
        => Provider.GetRequiredService<T>();

    public static T? GetService<T>(Type type) where T : class
        => Provider.GetRequiredService(type) as T;

}

