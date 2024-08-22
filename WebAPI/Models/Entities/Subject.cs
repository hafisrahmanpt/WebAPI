using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI.Models.Entities
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
    }
}
