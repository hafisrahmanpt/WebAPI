using WebAPI.Models.Entities;

namespace WebAPI.Services.Interfaces
{
    public interface ISubject
    {
        List<Subject> GetAllSubjects();
        Subject GetSubjectById(int id);
        Subject AddSubject(Subject subject);
        Subject UpdateSubject(Subject subject);
        void SoftDeleteSubject(int id);
        void HardDeleteSubject(int id);

        void AddStudentsToSubject(int subjectId, List<int> studentIds);
        List<Student> GetStudentsBySubjectId(int subjectId);
        bool IsSubjectExixts(string subjectName);
        bool IsSubjectExistsId(int subjectId);
    }
}
