using System.Text.Json.Serialization;

namespace WebAPI.Models.Entities
{
    public class StudentSubject
    {
        public int StudentId { get; set; }
        [JsonIgnore]
        public Student Student { get; set; }

        public int SubjectId { get; set; }
        [JsonIgnore]
        public Subject Subject { get; set; }

    }
}
