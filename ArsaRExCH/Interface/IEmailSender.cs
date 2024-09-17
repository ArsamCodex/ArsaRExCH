namespace ArsaRExCH.Interface
{
    public interface IEmailSender<TUser>
    {
        Task SendEmailAsync(TUser user, string subject, string htmlMessage);
    }
}
