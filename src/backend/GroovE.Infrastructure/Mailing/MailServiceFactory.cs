using GroovE.Application.Mailing;
using GroovE.Infrastructure.Mailing.LoggerService;
using GroovE.Infrastructure.Mailing.SendGridService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GroovE.Infrastructure.Mailing;

internal static class MailServiceFactory
{
    public static IMailService Create(IServiceProvider provider)
    {
        var configuration = provider.GetRequiredService<IOptions<MailingConfiguration>>().Value;

        return configuration.Service switch
        {
            MailService.Logger => ActivatorUtilities.CreateInstance<LoggerMailService>(provider),
            MailService.SendGrid => ActivatorUtilities.CreateInstance<SendGridMailService>(provider),
            _ => throw new ArgumentOutOfRangeException(nameof(configuration.Service), configuration.Service, "Unknown mail service type")
        };
    }
}
