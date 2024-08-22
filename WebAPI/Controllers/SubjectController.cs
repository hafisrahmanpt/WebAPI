using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.Entities;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : Controller
    {
        private readonly ISubject _subject;

        public SubjectController(ISubject isubject)
        {
            this._subject = isubject;
        }

        public class SaveResponse() 
        {
            public int SubjectID { get; set; }
        }

        [HttpGet]
        [Route("GetAllSubjects")]
        public IActionResult GetAllSubjects()
        {
            return Ok(_subject.GetAllSubjects());
        }

        [HttpGet]
        [Route("GetSubjectbyId/{id:int}")]
        public IActionResult GetSubjectById(int id)
        {
            try
            {
                Subject subject = _subject.GetSubjectById(id);
                if (subject != null)
                {
                    return Ok(subject);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }   

        [HttpPost]
        [Route("SaveSubject")]
        public IActionResult AddSubject(Subject subject)
        {
            var createdSubject = _subject.AddSubject(subject);
            return Ok(new SaveResponse() {SubjectID = createdSubject.SubjectId});
        }

        [HttpPatch]
        [Route("UpdateSubject")]
        public IActionResult UpdateSubject (Subject subject)
        {   
            var existingSubject = _subject.GetSubjectById(subject.SubjectId);

            if (existingSubject == null) 
            {
                return NotFound("No Subject Id found");
            }
            _subject.UpdateSubject(subject);
            return Ok(subject);
        }

        //softdelete Subject
        [HttpDelete]
        [Route("SoftDeleteSubject")]
        public IActionResult SoftDeleteSubject(int id)
        {
            
            var department = _subject.GetSubjectById(id);
            if (department == null)
            {
                return NotFound();
            }

            _subject.SoftDeleteSubject(id);

            return Ok();
        }

        //Hard delete Subject
        [HttpDelete]
        [Route("HardDeleteSubject")]

        public IActionResult HardDeleteSubject(int id)
        {

            var department = _subject.GetSubjectById(id);
            if (department == null)
            {
                return NotFound();
            }

            _subject.HardDeleteSubject(id);

            return Ok();
        }

        [HttpPost]
        [Route("AddSubjectWithStudents")]
        public IActionResult AddStudentsToSubject([FromBody] SubjectWithStudentsDTO dto)
        {
            if (_subject.IsSubjectExixts(dto.Subject.SubjectName))
            {
              return  BadRequest($"Subject '{dto.Subject.SubjectName}' already exists.");
            }

               var createdSubject = _subject.AddSubject(dto.Subject);

            if(dto.StudentIds != null && dto.StudentIds.Count>0)
            {
                _subject.AddStudentsToSubject(createdSubject.SubjectId, dto.StudentIds);
            }
            return CreatedAtAction(nameof(GetSubjectById), new {id=createdSubject.SubjectId},createdSubject);
        }

        [HttpGet]
        [Route("{subjectId}/Students")]
        public ActionResult<List<Student>> GetStudentsBySubjectId(int subjectId)
        {
            if (_subject.IsSubjectExistsId(subjectId))
            {
                var students = _subject.GetStudentsBySubjectId(subjectId);
                if (students == null || students.Count == 0)
                {
                    return NotFound($"No students found for subject with ID {subjectId}");
                }
                return Ok(students);
            }
            return BadRequest($"ID '{subjectId}' not exists.");
        }

    }
}
