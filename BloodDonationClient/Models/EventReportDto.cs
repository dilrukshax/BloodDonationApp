// File: BloodDonationClient/Models/EventReportDto.cs

namespace BloodDonationClient.Models
{
    public class EventReportDto
    {
        public int EventId { get; set; }

        public string EventTitle { get; set; }

        public int ParticipatedPeopleCount { get; set; }

        public decimal Cost { get; set; }

        public int APositive { get; set; }

        public int BPositive { get; set; }

        public int ABPositive { get; set; }

        public int OPositive { get; set; }

        public int ANegative { get; set; }

        public int BNegative { get; set; }

        public int ABNegative { get; set; }

        public int ONegative { get; set; }
    }
}
