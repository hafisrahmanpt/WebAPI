using System.Text.Json.Serialization;

namespace WebAPI.Models.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();

    }
}
