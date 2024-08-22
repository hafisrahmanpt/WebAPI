using Azure.Messaging;
using Microsoft.AspNetCore.Http.HttpResults;
using WebAPI.Data;
using WebAPI.Models.Entities;

namespace WebAPI.Repositories
{
    public class SubjectRepository
    {
        private readonly ApplicationDBContext dBContext;

        public SubjectRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        //get all subjects
        public List<Subject> GetAllSubjects()
        {
            return dBContext.subjects.ToList(); 
        }

        //get subject by id
        public Subject GetSubject(int id)
        {
            return dBContext.subjects.FirstOrDefault(x => x.SubjectId == id);
        }

        //Add subject
        public Subject AddSubject(Subject subject)
        {
            dBContext.subjects.Add(subject);
            dBContext.SaveChanges();
            return subject;
        }

        //update subject
        public Subject? UpdateSubject(Subject subject)
        {
            var update=  dBContext.subjects.FirstOrDefault(e=>e.SubjectId== subject.SubjectId);
            if (update != null)
            {
                update.SubjectId = subject.SubjectId;
                update.SubjectName = subject.SubjectName;
                update.IsActive = subject.IsActive;

                dBContext.SaveChanges();
                return update;
            }
            return null;
        }

        //softdelete subject
        public void SoftDeleteSubject(int id)
        {
            var subject= dBContext.subjects.FirstOrDefault(d => d.SubjectId == id);
             
            if(subject != null)
            {
                subject.IsActive = false;
                dBContext.SaveChanges();
            }
        }

        //delete subject
        public void HardDeleteSubject(int id)
        {
            var studentsubject = dBContext.studentSubjects.Where(s=>s.SubjectId == id).ToList();

            if (studentsubject.Any())
            {
                dBContext.studentSubjects.RemoveRange(studentsubject);
            }

            var subject = dBContext.subjects.FirstOrDefault(d => d.SubjectId == id);

            if (subject != null)
            {
                dBContext.subjects.Remove(subject);
                dBContext.SaveChanges();
            }
        }

        //Add multiple Studnts to a subject
        public void AddStudentsToSubject(int subjectId, List<int> studentIds)
        {
            foreach (var studentId in studentIds)
            {
                var studentSubject = new StudentSubject
                {
                    SubjectId = subjectId,
                    StudentId = studentId
                };
                dBContext.studentSubjects.Add(studentSubject);
            }
            dBContext.SaveChanges();
        }

        //Get All Students in a subject
        public List<Student> GetStudentsBySubjectId(int subjectid)
        {
            var IsSubidvalid = dBContext.subjects.FirstOrDefault(s => s.SubjectId == subjectid);

            if(IsSubidvalid != null)
            {
                var students = from Student in dBContext.students
                               join StudentSubject in dBContext.studentSubjects
                               on Student.StudentId equals StudentSubject.StudentId
                               where StudentSubject.SubjectId == subjectid
                               select Student;

                return students.ToList();
            }
            return new List<Student>();           
        }

        public bool IsSubjectExixts(string subjectName)
        {
            return dBContext.subjects.Any(d => d.SubjectName == subjectName);
        }

        public bool IsSubjectExistsId(int subjectId)
        {
            return dBContext.subjects.Any(s => s.SubjectId == subjectId);
        }

    }
}
