using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ValuesController(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }

        [HttpGet]
        [Route("/Doctor")]
        public async Task<IActionResult> GetDoctors() {
            var ret = await _context.Doctors.Select(e => new
            {
                id = e.IdDoctor,
                FirstName = e.FirstName,
                LastName = e.LastName,
                email = e.Email,
                /*prescriptions = e.Prescriptions.Select(e => new
                {
                    id = e.IdPrescription,
                    date = e.Date,
                    dueDate = e.DueDate,
                    idPatient = e.IdPatient,
                    patientName = e.Patient.FirstName,
                    prescriptionMedicaments = e.PrescriptionMedicaments.Select(e => new
                    {
                        IdMedicament = e.Medicament.IdMedicament,
                        MedicamentName = e.Medicament.Name,
                    })
                    .ToList(),
                })
                .ToList(),*/
            }).ToListAsync();
            return Ok(ret);
        }
        [HttpPost]
        [Route("/Doctor/AddDoctor")]
        public async Task<IActionResult> AddDoctor(DoctorPOST doctorPOST)
        {
            await _context.Doctors.AddAsync(new Doctor
            {
                FirstName = doctorPOST.FirstName,
                LastName= doctorPOST.LastName,
                Email = doctorPOST.Email,
            });
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("/Doctor/RemoveDoctor/{index}")]
        public async Task<IActionResult> RemoveDoctor(int index)
        {
            var doctor = _context.Doctors.Where(e => e.IdDoctor == index).FirstOrDefault();
            if (doctor != null) {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
                return Ok("Doctor removed");
            }
            return NotFound("There is no doctor with this id");
        }

        [HttpPut]
        [Route("/Doctor/UpdateDoctor/{index}")]
        public async Task<IActionResult> UpdateDoctor(int index, DoctorPUT doctorPUT)
        {
           //var doctor = await _context.Doctors.Where(e => e.IdDoctor == index).FirstOrDefaultAsync();
           // if (doctor == null) return NotFound("There is no doctor with this id"); 
            _context.Doctors.Update(new Doctor
            {
                IdDoctor = index,
                FirstName = doctorPUT.FirstName,
                LastName = doctorPUT.LastName,
                Email = doctorPUT.Email,
            });
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("/Prescription/{IdPrescription}")]
        public async Task<IActionResult> GetReceipt(int IdPrescription)
        {
           var prescription = await _context.Prescriptions.Where(e => e.IdPrescription == IdPrescription).Select(e => new
           {
               IdPrescription = e.IdPrescription,
               Date = e.Date,
               DueDate = e.DueDate,
               doctor = new
               {
                   e.Doctor.FirstName,
                   e.Doctor.LastName,
                   e.Doctor.Email
               },
               patient = new
               {
                   e.Patient.FirstName,
                   e.Patient.LastName,
                   e.Patient.Birthdate
               },
               prescriptionMedicaments = e.PrescriptionMedicaments.Select(e => new
               {
                   IdMedicament = e.Medicament.IdMedicament,
                   MedicamentName = e.Medicament.Name,
               })
               .ToList(),
           })
                .FirstOrDefaultAsync();
            if(prescription == null)
            {
                return NotFound("No prescription with such id");
            }
            return Ok(prescription);
        }
    }
}
