using CandidatesWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidatesWebApp.Repositories
{
    public interface ICandidateRepository
    {
        Task<IEnumerable<Candidate>> GetAllAsync();
        Task<Candidate> GetAsync(int id);
        Task CreateAsync(Candidate candidate);
        Task UpdateAsync(Candidate candidate);
        Task DeleteAsync(Candidate candidate);
        Task<IEnumerable<Candidate>> FindAsync(string firstName, string lastName, string emailAddress, string phoneNumber, string residentialZipCode);
        Task<bool> ExistsAsync(int id);
    }
}