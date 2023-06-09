namespace rtperson.Models
{
    public class ContactModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public ContactModel(Guid id, string type, string value)
        {
            Id = id;
            Type = type;
            Value = value;
        }
    }
}
