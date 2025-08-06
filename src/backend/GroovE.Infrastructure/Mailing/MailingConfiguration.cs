namespace GroovE.Infrastructure.Mailing;

public class MailingConfiguration
{
    public string From { get; set; }

    public Dictionary<string, string> TemplateMappings { get; set; } = [];

    public MailService MailService { get; set; }
}

public enum MailService
{
    Logger,
    Real
}
