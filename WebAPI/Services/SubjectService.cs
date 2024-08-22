using WebAPI.Data;
using WebAPI.Models.Entities;
using WebAPI.Repositories;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class SubjectService: ISubject
    {
        private readonly SubjectRepository _repository;

        public SubjectService(ApplicationDBContext dBContext)
        {
            _repository = new SubjectRepository(dBContext);
        }

        public List<Subject> GetAllSubjects()
        {
            return _repository.GetAllSubjects();
        }

        public Subject GetSubjectById(int id)
        {
            return _repository.GetSubject(id);
        }

        public Subject AddSubject(Subject subject) 
        {
            return _repository.AddSubject(subject);
        }

        public Subject? UpdateSubject(Subject subject)
        {
            return _repository.UpdateSubject(subject);
        }

        public void SoftDeleteSubject(int id)
        {
             _repository.SoftDeleteSubject(id);
        }

        public void HardDeleteSubject(int id)
        {
            _repository.HardDeleteSubject(id);
        }

        public void AddStudentsToSubject(int subjectId, List<int> studentIds)
        {
            _repository.AddStudentsToSubject(subjectId, studentIds);
        }

        public List<Student> GetStudentsBySubjectId(int studentId)
        {
            return _repository.GetStudentsBySubjectId(studentId);
        }

        public bool IsSubjectExixts(string subjectName)
        {
            return _repository.IsSubjectExixts(subjectName);
        }

        public bool IsSubjectExistsId(int subjectId)
        {
            return _repository.IsSubjectExistsId(subjectId);
        }
    }
}
