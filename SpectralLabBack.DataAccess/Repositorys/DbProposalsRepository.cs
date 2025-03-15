using Microsoft.EntityFrameworkCore;
using SpectralLabBack.DataAccess;

public class DbProposalsRepository
{
	private readonly SpectralLabDbContext _context;

	public DbProposalsRepository(SpectralLabDbContext context)
	{
		_context = context;
	}

	public async Task<List<Proposal>> GetAsync()
	{
		var proposals = await _context.Proposals.Include( p => p.ProposalSparesCount).ToListAsync();

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
		var newProposal = ToDbProposal(proposal);

		await _context.Proposals.AddAsync(newProposal);
		await _context.SaveChangesAsync();

		return newProposal.Id;
	}

	public async Task<Guid> RemoveAsync(Guid proposalId)
	{
		var proposal = await _context.Proposals.FirstAsync(p => p.Id == proposalId);

		_context.Proposals.Remove(proposal);

		await _context.SaveChangesAsync();

		return proposal.Id;
	}

	public async Task<Guid> UpdateAsync(Proposal newDataProposal)
	{
		var dbNewDataProposal = ToDbProposal(newDataProposal);
		var updatedProposal = await _context.Proposals.FindAsync(newDataProposal.Id);

		//var updatedProposal = proposals.First(p => p.Id == newDataProposal.Id);

		if (updatedProposal != null)
		{
			updatedProposal.CreateDate = dbNewDataProposal.CreateDate;
			updatedProposal.IsFinal = dbNewDataProposal.IsFinal;
			updatedProposal.Laboratory = dbNewDataProposal.Laboratory;
			updatedProposal.ProposalYearFor = dbNewDataProposal.ProposalYearFor;
			//updatedProposal.ProposalSparesCount = dbNewDataProposal.ProposalSparesCount;

			_context.SaveChanges();

			return updatedProposal.Id;
		}

		return new Guid();
	}

	public async Task<Proposal> FindAsync(Guid id)
	{
		var serchedProposal = await _context.Proposals.Include(p => p.ProposalSparesCount).FirstOrDefaultAsync(x => x.Id == id);

		return ToProposal(serchedProposal);
	}

	private DbProposal ToDbProposal(Proposal convertedProposal)
	{
		return new DbProposal
			{
				Id = convertedProposal.Id,
				Laboratory = convertedProposal.Laboratory,
				ProposalYearFor = convertedProposal.ProposalYearFor,
				CreateDate = convertedProposal.CreateDate,
				IsFinal = convertedProposal.IsFinal,
				ProposalSparesCount = convertedProposal.SparesCount.Select(p => new DbProposalSpareCount
				{
					Id = new Guid(),
					Count = p.Count,
					ReceivedCount = p.ReceivedCount,
					ProposalId = convertedProposal.Id,
					SpareId = p.SpareId
				}
				).ToList()
			};
	}

	private Proposal ToProposal(DbProposal convertedProposal)
	{
		return new Proposal(
			convertedProposal.Id,
			convertedProposal.Laboratory,
			convertedProposal.ProposalYearFor,
			convertedProposal.CreateDate,
			convertedProposal.IsFinal,
			convertedProposal.ProposalSparesCount.Select(s =>
				new ProposalSpareCount(
					s.Id,
					s.Count,
					s.ReceivedCount,
					s.SpareId
				)).ToList()
			);
	}


}


/*
	public async Task<Guid> UpdateAsync(Proposal newDataProposal)
	{
		var proposals = await _context.Proposals.Include(p => p.ProposalSparesCount).ToListAsync();

		var updatedProposal = proposals.First(p => p.Id == newDataProposal.Id);

		if (updatedProposal != null)
		{
			var proposal = ToDbProposal(newDataProposal);
			updatedProposal = proposal;

			_context.SaveChanges();

			return updatedProposal.Id;
		}

		return new Guid();
	}
*/