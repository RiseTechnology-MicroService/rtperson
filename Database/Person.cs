using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace rtperson.DatabaseModels
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string Company { get; set; } = null!;

        private List<Contact> _contacts = new List<Contact>();
        public List<Contact> Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value ?? new List<Contact>();
            }
        }
    }

    public class Contact
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = null!;

        public ContactType Type { get; set; }

        public Contact(string value, ContactType type)
        {
            Id = Guid.NewGuid();
            Value = value;
            Type = type;
        }
    }

    public enum ContactType
    {
        Phone,
        Email,
        Location
    }
}
