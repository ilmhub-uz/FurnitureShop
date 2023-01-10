namespace FurnitureShop.EmailSender.API.Services
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
