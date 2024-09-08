using PatientApi.Models;

namespace PatientApi.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetPatientsAsync(string birthDate, string birthDateFilter);
        Task<Patient> GetPatientByIdAsync(Guid id);
        Task<Patient> CreatePatientAsync(Patient patient);
        Task<bool> UpdatePatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(Guid id);
        Task<bool> PatientExistsAsync(Guid id);
    }
}
