using EMC.BuildingBlocks.Dtos;

namespace EMC.BuildingBlocks.Interfaces
{
    public interface IMailHelper
    {
        Task<TResponse> SendMailAsync(string toName, string toEmail, string subject, string body, Dictionary<string, string> smtpConfig);

    }
}
