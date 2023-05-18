using Exercise3.Models;
using Exercise3.Models.DTOs;
using Exercise3.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exercise3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _studentsRepository;
        public StudentsController(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if(!_studentsRepository.GetStudents().Any()) { 
            return Ok(new List<Student>());
            }
            try
            {
                return Ok(_studentsRepository.GetStudents());
            }catch(Exception ex) {
                return Problem();
            }
        }

        [HttpGet("{index}")]
        public async Task<IActionResult> Get(string index)
        {
            var student = _studentsRepository.GetStudents().Where(e => e.IndexNumber == index).FirstOrDefault();
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPut("{index}")]
        public async Task<IActionResult> Put(string index, StudentPUT newStudentData)
        {
            try
            {
                var student = _studentsRepository.GetStudents().Where(e => e.IndexNumber == index).FirstOrDefault();
                if (student is null)
                {
                    return NotFound();
                }
                await _studentsRepository.UpdateStudent(student, new Models.Student {
                    FirstName = newStudentData.FirstName,
                    LastName = newStudentData.LastName,
                    StudyName = newStudentData.StudyName,
                    StudyMode = newStudentData.StudyMode,
                    BirthDate = newStudentData.BirthDate,
                    Email = newStudentData.Email,
                    MothersName = newStudentData.MothersName,
                    FathersName = newStudentData.FathersName
                });    

                return Ok(newStudentData);
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(StudentPOST newStudent)
        {
            var student = _studentsRepository.GetStudents().Where(e => e.IndexNumber == newStudent.IndexNumber).FirstOrDefault();
            if (student is null)
            {
                await _studentsRepository.AddStudent(new Student { FirstName = newStudent.FirstName,
                    LastName = newStudent.LastName,
                    StudyName = newStudent.StudyName,
                    StudyMode = newStudent.StudyMode,
                    IndexNumber = newStudent.IndexNumber,
                    BirthDate = newStudent.BirthDate,
                    Email = newStudent.Email,
                    MothersName = newStudent.MothersName,
                    FathersName = newStudent.FathersName });
                return Created($"/api/animals/{newStudent.IndexNumber}",newStudent);
            }
            return Conflict();
        }

        [HttpDelete("{index}")]
        public async Task<IActionResult> Delete(string index)
        {
            try
            {
                var student = _studentsRepository.GetStudents().Where(e => e.IndexNumber == index).FirstOrDefault();
                if (student is null)
                {
                    return NotFound();
                }
                _studentsRepository.DeleteStudent(student);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem();
            }
        } 

    }
}
