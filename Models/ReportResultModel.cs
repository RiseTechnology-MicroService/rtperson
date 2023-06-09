namespace rtperson.Models
{
    public class ReportResultModel
    {
        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }

        public ReportResultModel(int personCount, int phoneNumberCount)
        {
            PersonCount = personCount;
            PhoneNumberCount = phoneNumberCount;
        }
    }
}
