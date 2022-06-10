using CandidatesWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CandidatesWebApp.Data
{
    /// <summary>
    /// Represents the context for the Candidates database.
    /// </summary>
    public class CandidatesWebAppDbContext : DbContext
    {
        public CandidatesWebAppDbContext() : base("name=CandidatesWebAppDbContext")
        {
            Database.SetInitializer(new CandidatesWebAppInitializer());
        }

        /// <summary>
        /// Represents the table Candidate.
        /// </summary>
        public virtual DbSet<Candidate> Candidates { get; set; }
    }
}