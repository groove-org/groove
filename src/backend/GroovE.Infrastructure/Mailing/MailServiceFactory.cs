using GroovE.Application.Mailing;
using GroovE.Infrastructure.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GroovE.Infrastructure.Mailing;

internal static class MailServiceFactory
{
    public static IMailService Create(IServiceProvider provider)
    {
        var configuration = provider.GetRequiredService<MailingConfiguration>();

        return configuration.MailService switch
        {
            MailService.Logger => provider.GetRequiredService<LoggerMailService>(),
            MailService.Real => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(configuration.MailService), configuration.MailService, "Unknown mail service type")
        };
    }
}
