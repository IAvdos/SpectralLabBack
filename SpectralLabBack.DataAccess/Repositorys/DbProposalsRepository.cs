using Microsoft.EntityFrameworkCore;
using SpectralLabBack.DataAccess;

public class DbProposalsRepository
{
	private readonly SpectralLabDbContext _context;

	public DbProposalsRepository(SpectralLabDbContext context)
	{
		_context = context;
	}

	public async Task<List<Proposal>> Get()
	{
		var proposals =  _context.Proposals.ToList();

		return proposals.Select(p =>
			new Proposal(
				p.Id,
				p.Laboratory,
				p.ProposalYearFor,
				p.CreateDate,
				p.IsFinal,
				p.ProposalSparesCount.Select(s =>
					new ProposalSpareCount(
						s.Id,
						s.Count,
						s.ReceivedCount,
						s.SpareId
					)).ToList()
				)).ToList();
	}

	public async Task<Guid> CreateAsync(Proposal proposal)
	{
		var newProposal = new DbProposal
		{
			Id = proposal.Id,
			Laboratory = proposal.Laboratory,
			ProposalYearFor = proposal.ProposalYearFor,
			CreateDate = proposal.CreateDate,
			IsFinal = proposal.IsFinal,
			ProposalSparesCount = proposal.SparesCount.Select(p => new DbProposalSpareCount
			{
				Id = new Guid(),
				Count = p.Count,
				ReceivedCount = p.ReceivedCount,
				ProposalId = proposal.Id,
				SpareId = p.SpareId
			}
			).ToList()
		};

		await _context.Proposals.AddAsync(newProposal);
		await _context.SaveChangesAsync();

		return proposal.Id;
	}

	public async Task<Guid> RemoveAsync(Guid proposalId)
	{
		var proposal = await _context.Proposals.FirstAsync(p => p.Id == proposalId);

		_context.Proposals.Remove(proposal);

		await _context.SaveChangesAsync();

		return proposal.Id;
	}


}