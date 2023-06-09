namespace rtperson.Models
{
    public class PersonSummaryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public PersonSummaryModel(Guid id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }
    }
}