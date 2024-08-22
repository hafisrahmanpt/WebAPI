using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.Entities;
using WebAPI.Services.Interfaces;

namespace WebAPI.Repositories
{
    public class StudentRepository
    {
        private readonly ApplicationDBContext dBContext;

        public StudentRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        //get all students
        public List<Student> GetAllStudents()
        {
            return dBContext.students.ToList();
        }

        //get student by id
        public Student? GetStudentById(int id)
        {
            return dBContext.students.FirstOrDefault(x => x.StudentId == id);
        }

        //add student
        public Student AddStudent(Student student)
        {
            dBContext.students.Add(student);
            dBContext.SaveChanges();
            return student;
        }

        //update student 
        public Student? UpdateStudent(Student student)
        {
            var updatestudent = dBContext.students.FirstOrDefault(e=>e.StudentId == student.StudentId);
            if (updatestudent!= null)
            {
                updatestudent.StudentName = student.StudentName;
                updatestudent.IsActive = student.IsActive;
                //updatestudent.SubjectId = student.SubjectId;

                dBContext.SaveChanges();
                return updatestudent;
            }
            return null;
        }

        //soft delete students
        public void SoftDeleteStudent(int id)
        {
            var student = dBContext.students.FirstOrDefault(e => e.StudentId == id);
            if (student != null)
            {
                student.IsActive = false;
                dBContext.SaveChanges();
            }
        }

        //hard delete Students
        public void HardDeleteStudent(int id)
        {
            var studentSubjects = dBContext.studentSubjects.Where(ss => ss.StudentId == id).ToList();

            if (studentSubjects.Any())
            {
                dBContext.studentSubjects.RemoveRange(studentSubjects);
            }

            var student = dBContext.students.FirstOrDefault(e => e.StudentId == id);

            if (student != null)
            {
                dBContext.students.Remove(student);
                dBContext.SaveChanges();
            }
        }

        //Add multiple subjects to a Student
        public void AddSubjectsToStudent(int studentId, List<int> subjectIds)
        {
            foreach (var subjectId in subjectIds)
            {
                var studentSubject = new StudentSubject
                {
                    StudentId = studentId,
                    SubjectId = subjectId
                };
                dBContext.studentSubjects.Add(studentSubject);
            }
            dBContext.SaveChanges();
        }

        //Get All Takencourses by a student
        public List<Subject> GetSubjectsByStudentId(int studentId)
        {
            var subjects = from Subject in dBContext.subjects
                           join StudentSubject in dBContext.studentSubjects
                           on Subject.SubjectId equals StudentSubject.SubjectId
                           where StudentSubject.StudentId == studentId
                           select Subject;
            return subjects.ToList();
        }

        //check if the student already exists
        public bool IsStudentExixts(string studentName)
        {
           return dBContext.students.Any(s=> s.StudentName == studentName);
        }

        public bool IsStudentExistsId(int StudentId)
        {
            return dBContext.students.Any(s => s.StudentId == StudentId);
        }
    }
}
