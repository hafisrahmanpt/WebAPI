using WebAPI.Models.Entities;

namespace WebAPI.Models
{
    public class StudentwithSubjectsDTO
    {
        public Student Student { get; set; }
        public List<int> SubjectIds { get; set; }
    }
}
