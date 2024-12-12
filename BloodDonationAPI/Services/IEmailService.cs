// File: BloodDonationAPI/Services/IEmailService.cs

using System.Threading.Tasks;

namespace BloodDonationAPI.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
