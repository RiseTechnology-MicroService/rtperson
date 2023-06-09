using rtperson.DatabaseModels;

namespace rtperson.Models
{
    public class CreateContactModel
    {
        public ContactType Type { get; set; }
        public string Value { get; set; }

        public CreateContactModel(ContactType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
