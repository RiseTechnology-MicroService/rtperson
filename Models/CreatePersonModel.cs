namespace rtperson.Models
{
    public class CreatePersonModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        public CreatePersonModel(string name, string surname, string company)
        {
            Name = name;
            Surname = surname;
            Company = company;
        }
    }
}
