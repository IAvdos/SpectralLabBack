using Microsoft.EntityFrameworkCore;
using SpectralLabBack.DataAccess;

public class DbProposalSparesRepository
{
	private readonly SpectralLabDbContext _context;

	public DbProposalSparesRepository(SpectralLabDbContext context)
	{
		_context = context;
	}


	public async Task<List<ProposalSpareCount>> GetAsync()
	{
		var spares = await _context.ProposalsSpareCount.ToListAsync();

		return spares.Select( s => 
			new ProposalSpareCount
				(
					s.Id,
					s.Count,
					s.ReceivedCount,
					s.SpareId
				)).ToList();
	}


	public async Task UpdateListAsync(List<ProposalSpareCount> proposalSpares, Guid proposalId)
	{
		var newProposalSpares = proposalSpares.Where( p => p.Id == Guid.Empty ).ToList();
		await AddListAsync(newProposalSpares, proposalId);

		var proposalSparesInDb = _context.ProposalsSpareCount.Where(p => p.ProposalId == proposalId).ToList();
		//IQueryable<DbProposalSpareCount> updatedProposalSpares = null;
		//IQueryable<DbProposalSpareCount> removedProposalSpares = null;

		foreach(var spare in proposalSparesInDb)
		{
			var currentSpare = proposalSpares.FirstOrDefault( p => p.SpareId == spare.SpareId);

			if (currentSpare == null)
			{
				_context.ProposalsSpareCount.Remove(spare);
			}
			else
			{
				spare.Count = currentSpare.Count;
				spare.ReceivedCount = currentSpare.ReceivedCount;
			}
		}

		await _context.SaveChangesAsync();
	}


	public async Task AddListAsync(List<ProposalSpareCount> proposalSpares, Guid proposalId)
	{
		var dbProposalSpares = proposalSpares.Select(p => new DbProposalSpareCount
		{
			Id = Guid.NewGuid(),
			Count = p.Count,
			ReceivedCount = p.ReceivedCount,
			ProposalId = proposalId,
			SpareId = p.SpareId
		}).ToList();

		await _context.ProposalsSpareCount.AddRangeAsync(dbProposalSpares);

		_context.SaveChanges();
	}
}