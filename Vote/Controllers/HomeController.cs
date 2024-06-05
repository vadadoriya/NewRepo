using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Vote.Models;
using Vote.ViewModel;

namespace Vote.Controllers;
public class HomeController : Controller
{
    private readonly VotingContext _context;

    public HomeController(VotingContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var model = new VotingViewModel
        {
            Voters = await _context.Voters.ToListAsync(),
            Candidates = await _context.Candidates.ToListAsync()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddVoter(string name)
    {
        var voter = new Voter { Name = name };
        _context.Voters.Add(voter);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AddCandidate(string name)
    {
        var candidate = new Candidate { Name = name };
        _context.Candidates.Add(candidate);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> CastVote(int voterId, int candidateId)
    {
        var voter = await _context.Voters.FindAsync(voterId);
        if (voter == null || voter.HasVoted)
        {
            return BadRequest("Invalid voter.");
        }

        var candidate = await _context.Candidates.FindAsync(candidateId);
        if (candidate == null)
        {
            return BadRequest("Invalid candidate.");
        }

        voter.HasVoted = true;
        candidate.Votes++;
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
