using MongoDB.Driver;

namespace rtperson.DatabaseModels.Settings
{
    public static class PersonSettings
    {
        public static void ApplyOptions(this IMongoCollection<Person> collection)
        {
            // Henüz bir kısıtlama yok
            return;
        }
    }
}