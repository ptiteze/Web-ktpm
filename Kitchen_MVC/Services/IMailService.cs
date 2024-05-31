using Kitchen_MVC.DTO.Mail;

namespace Kitchen_MVC.Services
{
    public interface IMailService
    {
        void sendMail(CreateMailRequest request);
    }
}
