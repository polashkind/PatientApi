using Microsoft.AspNetCore.Mvc;
using PatientApi.Models;
using PatientApi.Repositories;

namespace PatientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _repository;

        public PatientController(IPatientRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients(
            [FromQuery] string birthDate,
            [FromQuery] string birthDateFilter)
        {
            try
            {
                var patients = await _repository.GetPatientsAsync(birthDate, birthDateFilter);
                return Ok(patients);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            if (string.IsNullOrWhiteSpace(patient.Name.Family) || patient.BirthDate == DateTime.MinValue)
            {
                return BadRequest("Family name and birth date are required.");
            }

            var createdPatient = await _repository.CreatePatientAsync(patient);
            return CreatedAtAction(nameof(GetPatients), new { id = createdPatient.Id }, createdPatient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(Guid id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            var updated = await _repository.UpdatePatientAsync(patient);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            var deleted = await _repository.DeletePatientAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
