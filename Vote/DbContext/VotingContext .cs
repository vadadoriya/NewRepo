using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Vote.Models;

public class VotingContext : DbContext
{
    public DbSet<Voter> Voters { get; set; }
    public DbSet<Candidate> Candidates { get; set; }

    public VotingContext(DbContextOptions<VotingContext> options) : base(options)
    {
    }
}
