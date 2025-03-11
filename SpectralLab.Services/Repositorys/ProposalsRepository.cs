public class ProposalsRepository
{
	private readonly DbProposalsRepository _dbRepository;

	public ProposalsRepository(DbProposalsRepository dbRepository)
	{
		_dbRepository = dbRepository;
	}

	public async Task<Guid> CreateAsync(NewProposalRequest requestProposal)
	{
		var newProsal = new Proposal
			(
				new Guid(),
				requestProposal.Laboratory,
				requestProposal.ProposalYearFor,
				requestProposal.CreateDate,
				requestProposal.IsFinal,
				requestProposal.SparesCount.Select(proposal =>
					new ProposalSpareCount
					(
						new Guid(),
						proposal.Count,
						proposal.ReceivedCount,
						proposal.SpareId
					)).ToList()
			);

		return await _dbRepository.CreateAsync( newProsal );
	}

	public async Task<Guid> RemoveAsync( Guid proposalId)
	{
		return await _dbRepository.RemoveAsync( proposalId );
	}

	public async Task<List<Proposal>> GetProposals()
	{
		return await _dbRepository.Get();
	}
}