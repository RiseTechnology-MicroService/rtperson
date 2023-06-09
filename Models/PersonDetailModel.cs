namespace rtperson.Models
{
    public class PersonDetailModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        public List<ContactModel> Contacts { get; set; }

        public PersonDetailModel(string name, string surname, string company, List<ContactModel> contacts)
        {
            Name = name;
            Surname = surname;
            Company = company;
            Contacts = contacts;
        }
    }
}
