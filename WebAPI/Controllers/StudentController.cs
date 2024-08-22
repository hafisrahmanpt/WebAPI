using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.JavaScript;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.Entities;
using WebAPI.Services;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudent _student;

        public StudentController(IStudent istudent)
        {
            this._student = istudent;
        }

        public class SaveResponse
        {
            public int StudentID { get; set; }
        }

        //get all students
        [HttpGet]
        [Route("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            return Ok(_student.GetAllStudents());
        }

        //get student by id
        [HttpGet]
        [Route("GetStudentByID/{id:int}")]
        public IActionResult GetStudentById(int id)
        {
            try
            {
                Student objstudent = _student.GetStudentById(id);
                if (objstudent != null)
                {
                    return Ok(objstudent);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }

        //post/save students
        [HttpPost]
        [Route("SaveStudent")]
        public  IActionResult AddStudent(Student student)
        {
            Student createdStudent = _student.AddStudent(student);
            return Ok(new SaveResponse() { StudentID = createdStudent.StudentId});
        }

        //update students
        [HttpPatch]
        [Route("UpdateStudent")]
        public IActionResult UpdateStudent(Student student)
        {
            var existingStudent = _student.GetStudentById(student.StudentId);

            if (existingStudent == null)
            {
                return NotFound("No Student Id found");
            }

            _student.UpdateStudent(student);
            return Ok(student);
        }

        //soft delete Student
        [HttpDelete]
        [Route("SoftDeleteStudent")]
        public IActionResult SoftDeleteStudent(int id)
        {

            var department = _student.GetStudentById(id);
            if (department == null)
            {
                return NotFound();
            }

            _student.SoftDeleteStudent(id);

            return Ok();
        }

        //Hard delete Student
        [HttpDelete]
        [Route("HardDeleteStudent")]

        public IActionResult HardDeleteStudent(int id)
        {

            var department = _student.GetStudentById(id);
            if (department == null)
            {
                return NotFound();
            }

            _student.HardDeleteStudent(id);

            return Ok();
        }

        [HttpPost]
        [Route("AddStudentWithSubjects")]
        public IActionResult AddStudentWithSubjects([FromBody] StudentwithSubjectsDTO dto)
        {
            if (_student.IsStudentExixts(dto.Student.StudentName))
            {
               return BadRequest($"Student '{dto.Student.StudentName}' already exists.");
            }

                var createdStudent = _student.AddStudent(dto.Student);
            
            if (dto.SubjectIds != null && dto.SubjectIds.Count > 0)
            {
                _student.AddSubjectsToStudent(createdStudent.StudentId, dto.SubjectIds);
            }

            return CreatedAtAction(nameof(GetStudentById), new { id = createdStudent.StudentId }, createdStudent);
        }

        [HttpGet]
        [Route("{studentId}/Subjects")]
        public ActionResult<List<Subject>>GetSubjectsByStudentId(int studentId)
        {
            if (_student.IsStudentExistsId(studentId))
            {
                var subjects = _student.GetSubjectsByStudentId(studentId);
                if (subjects == null || subjects.Count == 0)
                {
                    return NotFound($"No subjects found for student with ID {studentId}");
                }
                return Ok(subjects);
            }
            return BadRequest($"ID '{studentId}' not exists.");
        }
    }
}
