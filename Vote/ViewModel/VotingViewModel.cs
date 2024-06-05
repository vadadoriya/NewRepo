using Vote.Models;

namespace Vote.ViewModel;
public class VotingViewModel
{
    public List<Voter> Voters { get; set; }
    public List<Candidate> Candidates { get; set; }
}

