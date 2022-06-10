using CandidatesWebApp.Data;
using CandidatesWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CandidatesWebApp.Repositories
{
    /// <summary>
    /// Defines methods for Candidate repository.
    /// </summary>
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CandidatesWebAppDbContext _context;

        public CandidateRepository(CandidatesWebAppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the Candidates.
        /// </summary>
        /// <returns>Candidates.</returns>
        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            var candidates = await _context.Candidates.ToListAsync();
            return candidates;
        }

        /// <summary>
        /// Gets a Candidate by Id.
        /// </summary>
        /// <param name="id">Id of the Candidate.</param>
        /// <returns>Candidate</returns>
        public async Task<Candidate> GetAsync(int id)
        {
            var candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.Id == id);
            return candidate;
        }

        /// <summary>
        /// Creates a new Candidate.
        /// </summary>
        /// <param name="candidate">Candidate to be created.</param>
        /// <returns></returns>
        public async Task CreateAsync(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing Candidate.
        /// </summary>
        /// <param name="candidate">Candidate to be updated.</param>
        /// <returns></returns>
        public async Task UpdateAsync(Candidate candidate)
        {
            _context.Entry(candidate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an existing Candidate.
        /// </summary>
        /// <param name="candidate">Candidate to be deleted.</param>
        /// <returns></returns>
        public async Task DeleteAsync(Candidate candidate)
        {
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Searches for Candidates that match with the criteria fields.
        /// </summary>
        /// <param name="firstName">Text to be searched if it is contained in First Name.</param>
        /// <param name="lastName">Text to be searched if it is contained in Last Name.</param>
        /// <param name="emailAddress">Text to be searched if it is contained in Email Address.</param>
        /// <param name="phoneNumber">Text to be searched if it is contained in Phone Number.</param>
        /// <param name="residentialZipCode">Text to be searched if it is contained in First Residential Zip Code.</param>
        /// <returns>Candidates</returns>
        public async Task<IEnumerable<Candidate>> FindAsync(string firstName, string lastName, string emailAddress, string phoneNumber, string residentialZipCode)
        {
            try
            {
                var query = (from item in _context.Candidates
                             where (!string.IsNullOrEmpty(firstName) && item.FirstName.Contains(firstName))
                                 || (!string.IsNullOrEmpty(lastName) && item.LastName.Contains(lastName))
                                 || (!string.IsNullOrEmpty(emailAddress) && item.EmailAddress.Contains(emailAddress))
                                 || (!string.IsNullOrEmpty(phoneNumber) && item.PhoneNumber.Contains(phoneNumber))
                                 || (!string.IsNullOrEmpty(residentialZipCode) && item.ResidentialZipCode.Contains(residentialZipCode))
                             select item);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Verifies if a Candidate exists by the Id.
        /// </summary>
        /// <param name="id">Id of the Candidate.</param>
        /// <returns>True or False</returns>
        public async Task<bool> ExistsAsync(int id)
        {
            var exists = await _context.Candidates.AnyAsync(x => x.Id == id);
            return exists;
        }
    }
}