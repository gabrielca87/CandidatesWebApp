using CandidatesWebApp.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace CandidatesWebApp.Data
{
    /// <summary>
    /// Defines the seed method to initialize the database.
    /// </summary>
    public class CandidatesWebAppInitializer : CreateDatabaseIfNotExists<CandidatesWebAppDbContext>
    {
        /// <summary>
        /// Seeds initial records when the database is created.
        /// </summary>
        /// <param name="context">DBContext</param>
        protected override void Seed(CandidatesWebAppDbContext context)
        {
            var candidates = new List<Candidate>();

            candidates.Add(new Candidate() { FirstName = "Gabriel", LastName = "Coca", EmailAddress = "gabrielcoca87@gmail.com", PhoneNumber = "111-111-1111", ResidentialZipCode = "12345" });
            candidates.Add(new Candidate() { FirstName = "John", LastName = "Smith", EmailAddress = "john.smith@gmail.com", PhoneNumber = "222-222-2222", ResidentialZipCode = "2222" });
            candidates.Add(new Candidate() { FirstName = "Camila", LastName = "Aragon", EmailAddress = "c.aragon@outlook.com", PhoneNumber = "444-555-6666", ResidentialZipCode = "00000" });

            context.Candidates.AddRange(candidates);

            base.Seed(context);
        }
    }
}