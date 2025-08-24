using Microsoft.Extensions.Configuration;

namespace GroovE.Application.Common;

public static class ConfigurationExtensions
{
    public static IConfigurationSection GetSectionWithoutSuffix<TSection>(this IConfiguration configuration) where TSection : class
    {
        var sectionName = typeof(TSection).Name;
        if (sectionName.EndsWith("Configuration", StringComparison.Ordinal))
        {
            sectionName = sectionName[..^"Configuration".Length];
        }

        return configuration.GetSection(sectionName);
    }
}
