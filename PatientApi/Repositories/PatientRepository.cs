using Microsoft.EntityFrameworkCore;
using PatientApi.Data;
using PatientApi.Models;

namespace PatientApi.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientContext _context;

        public PatientRepository(PatientContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync(string birthDate, string birthDateFilter)
        {
            var query = _context.Patients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(birthDate) && !string.IsNullOrWhiteSpace(birthDateFilter))
            {
                if (DateTime.TryParse(birthDate, out DateTime parsedDate))
                {
                    switch (birthDateFilter)
                    {
                        case "eq":
                            query = query.Where(p => p.BirthDate.Date == parsedDate.Date);
                            break;
                        case "ne":
                            query = query.Where(p => p.BirthDate.Date != parsedDate.Date);
                            break;
                        case "lt":
                            query = query.Where(p => p.BirthDate.Date < parsedDate.Date);
                            break;
                        case "gt":
                            query = query.Where(p => p.BirthDate.Date > parsedDate.Date);
                            break;
                        case "le":
                            query = query.Where(p => p.BirthDate.Date <= parsedDate.Date);
                            break;
                        case "ge":
                            query = query.Where(p => p.BirthDate.Date >= parsedDate.Date);
                            break;
                        case "sa":
                            query = query.Where(p => p.BirthDate.Date > parsedDate.Date);
                            break;
                        case "eb":
                            query = query.Where(p => p.BirthDate.Date < parsedDate.Date);
                            break;
                        case "ap":
                            query = query.Where(p => p.BirthDate.Date >= parsedDate.AddDays(-3) &&
                                                     p.BirthDate.Date <= parsedDate.AddDays(3));
                            break;
                        default:
                            throw new ArgumentException("Invalid birthDate filter.");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid birthDate format.");
                }
            }

            return await query.ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(Guid id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            patient.Id = Guid.NewGuid();
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<bool> UpdatePatientAsync(Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PatientExistsAsync(patient.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeletePatientAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return false;
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PatientExistsAsync(Guid id)
        {
            return await _context.Patients.AnyAsync(e => e.Id == id);
        }
    }
}
