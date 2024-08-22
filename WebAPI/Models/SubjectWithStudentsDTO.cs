using WebAPI.Models.Entities;

namespace WebAPI.Models
{
    public class SubjectWithStudentsDTO
    {
        public Subject Subject { get; set; }
        public List<int> StudentIds { get; set; }
    }
}
