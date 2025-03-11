using Microsoft.Extensions.Logging;

public class ProposalsRepository
{
	private readonly DbProposalsRepository _dbRepository;
	private readonly ILogger<ProposalsRepository> _loger;

	public ProposalsRepository(DbProposalsRepository dbRepository, ILogger<ProposalsRepository> loger)
	{
		_dbRepository = dbRepository;
		_loger = loger;
	}

	public async Task<Guid> CreateAsync(Proposal requestProposal)
	{
		return await _dbRepository.CreateAsync(requestProposal);
	}

	public async Task<Guid> RemoveAsync( Guid proposalId)
	{
		return await _dbRepository.RemoveAsync( proposalId );
	}

	public async Task<List<Proposal>> GetProposals()
	{
		return await _dbRepository.GetAsync();
	}

	public async Task<Guid> UpdateProposalAsync(Proposal proposal)
	{
		return await _dbRepository.UpdateAsync(proposal);
	}
}