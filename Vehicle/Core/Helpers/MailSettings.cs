namespace Core.Helpers;

public class MailSettings
{
    public string From { get; set; }
    public string Password { get; set; } 
    public string Host { get; set; }
    public int Port { get; set; }
    public bool UseSSL { get; set; }
}