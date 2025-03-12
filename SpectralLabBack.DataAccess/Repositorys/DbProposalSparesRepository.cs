using SpectralLabBack.DataAccess;

public class DbProposalSparesRepository
{
	private readonly SpectralLabDbContext _context;

	public DbProposalSparesRepository(SpectralLabDbContext context)
	{
		_context = context;
	}
	public async void UpdateListAsync(List<ProposalSpareCount> proposalSpares, Guid proposalId)
	{
		var newProposalSpares = proposalSpares.Where( p => p.Id == Guid.Empty ).ToList();
		AddListAsync(newProposalSpares, proposalId);

		var proposalSparesInDb = _context.ProposalsSpareCount.Where(p => p.Id == proposalId);
		IQueryable<DbProposalSpareCount> updatedProposalSpares = null;
		IQueryable<DbProposalSpareCount> removedProposalSpares = null;

		foreach(var spare in proposalSpares)
		{
			var currentSpare = proposalSparesInDb.First( p => p.SpareId == spare.Id);

			if (currentSpare != null)
			{
				_context.ProposalsSpareCount.Remove(currentSpare);
			}
			else
			{
				currentSpare.Count = spare.Count;
				currentSpare.ReceivedCount = spare.ReceivedCount;
			}
		}

		await _context.SaveChangesAsync();
	}

	public async void AddListAsync(List<ProposalSpareCount> proposalSpares, Guid proposalId)
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

	public async void RemoveListAsync(List<ProposalSpareCount> proposalSpares)
	{

	}
}