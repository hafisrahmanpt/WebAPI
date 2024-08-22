using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.Entities;
using WebAPI.Repositories;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class StudentService : IStudent
    {
        private readonly StudentRepository _repository;

        public StudentService(ApplicationDBContext dBContext)
        {
            _repository = new StudentRepository(dBContext);
        }

        public List<Student> GetAllStudents()
        {
            return _repository.GetAllStudents();
        }

        public Student? GetStudentById(int id)
        {
            return _repository.GetStudentById(id);
        }

        public Student AddStudent(Student student)
        {
            return _repository.AddStudent(student);
        }

        public Student? UpdateStudent(Student student)
        {
            return _repository.UpdateStudent(student);  
        }

        public void SoftDeleteStudent(int id)
        {
            _repository.SoftDeleteStudent(id);
        }

        public void HardDeleteStudent(int id)
        {
            _repository.HardDeleteStudent(id);
        }


        public void AddSubjectsToStudent(int studentId, List<int> subjectIds)
        {
            _repository.AddSubjectsToStudent(studentId, subjectIds);
        }

        public List<Subject> GetSubjectsByStudentId(int studentId)
        {
           return  _repository.GetSubjectsByStudentId(studentId);
        }

        public bool IsStudentExixts(string studentName)
        {
            return _repository.IsStudentExixts(studentName);
        }

        public bool IsStudentExistsId(int studentId)
        {
            return _repository.IsStudentExistsId(studentId);
        }

        //public List<Student> SearchStudentsBySubjectName(string subjectName)
        //{
        //    return _repository.SearchStudentsBySubjectName(subjectName);
        //}
    }
}
