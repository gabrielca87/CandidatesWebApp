using CandidatesWebApp.Models;
using CandidatesWebApp.Repositories;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CandidatesWebApp.Controllers
{
    /// <summary>
    /// Defines methods for Candidates controller.
    /// </summary>
    [Route("api/candidates")]
    public class CandidatesController : ApiController
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidatesController(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        /// <summary>
        /// Gets all Candidates.
        /// </summary>
        /// <returns>Candidates</returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var candidates = await _candidateRepository.GetAllAsync();
            if (candidates == null)
            {
                return NotFound();
            }

            return Ok(candidates);
        }

        /// <summary>
        /// Gets a Candidate by Id.
        /// </summary>
        /// <param name="id">Id of the Candidate.</param>
        /// <returns>Candidate</returns>
        [HttpGet]
        [Route("api/candidates/{id}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid Id.");
            }

            var card = await _candidateRepository.GetAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            return Ok(card);
        }

        /// <summary>
        /// Creates a new Candidate.
        /// </summary>
        /// <param name="candidate">Candidate to be created.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Create(Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _candidateRepository.CreateAsync(candidate);

            return Ok();
        }

        /// <summary>
        /// Updates an existing Candidate.
        /// </summary>
        /// <param name="candidate">Candidate to be updated.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> Update(Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (candidate.Id == 0)
            {
                return BadRequest("Invalid Id.");
            }

            var exists = await _candidateRepository.ExistsAsync(candidate.Id.Value);
            if (!exists)
            {
                return NotFound();
            }

            await _candidateRepository.UpdateAsync(candidate);

            return Ok();
        }

        /// <summary>
        /// Deletes an existing Candidate.
        /// </summary>
        /// <param name="id">Id of the Candidate to be deleted.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid Id.");
            }

            var candidate = await _candidateRepository.GetAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            await _candidateRepository.DeleteAsync(candidate);

            return Ok();
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
        [HttpGet]
        [Route("api/candidates/search")]
        public async Task<IHttpActionResult> Search(string firstName = null, string lastName = null, string emailAddress = null, string phoneNumber = null, string residentialZipCode = null)
        {
            var candidates = await _candidateRepository.FindAsync(firstName, lastName, emailAddress, phoneNumber, residentialZipCode);

            if (candidates == null && !candidates.Any())
            {
                return NotFound();
            }

            return Ok(candidates);
        }
    }
}
