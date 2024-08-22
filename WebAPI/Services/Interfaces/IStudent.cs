using WebAPI.Models;
using WebAPI.Models.Entities;

namespace WebAPI.Services.Interfaces
{
    public interface IStudent
    {
        List<Student> GetAllStudents();
        Student GetStudentById(int id);
        Student AddStudent(Student student);
        Student UpdateStudent(Student student);
        void SoftDeleteStudent(int id);
        void HardDeleteStudent(int id);

        void AddSubjectsToStudent(int studentId, List<int> subjectIds);
       List<Subject> GetSubjectsByStudentId(int studentId);
        bool IsStudentExixts(string studentName);
        bool IsStudentExistsId(int studentId);
    }
}
