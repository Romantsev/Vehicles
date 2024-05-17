using Core.Models;

namespace BLL.Interfaces;

public interface IMailService
{
    void SendEmail(Operation operation , string email);
}

