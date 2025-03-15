using Microsoft.Extensions.Logging;

public class ProposalsRepository
{
	private readonly DbProposalsRepository _dbProposalRepository;
	private readonly DbProposalSparesRepository _dbProposalSparesRepository;
	private readonly ILogger<ProposalsRepository> _loger;

	public ProposalsRepository(DbProposalsRepository dbproposalRepository, DbProposalSparesRepository dbProposalSparesRepository, ILogger<ProposalsRepository> loger)
	{
		_dbProposalRepository = dbproposalRepository;
		_dbProposalSparesRepository = dbProposalSparesRepository;
		_loger = loger;
	}

	public async Task<Guid> CreateAsync(Proposal requestProposal)
	{
		return await _dbProposalRepository.CreateAsync(requestProposal);
	}

	public async Task<Guid> RemoveAsync( Guid proposalId)
	{
		return await _dbProposalRepository.RemoveAsync( proposalId );
	}

	public async Task<List<Proposal>> GetProposals()
	{
		return await _dbProposalRepository.GetAsync();
	}

	public async Task<Guid> UpdateProposalAsync(Proposal proposal)
	{
		var proposalSpares = proposal.SparesCount;

		var result = await _dbProposalRepository.UpdateAsync(proposal);

		if (result != Guid.Empty)
		{
			await _dbProposalSparesRepository.UpdateListAsync(proposalSpares, result);
		}
		
		return result;
	}
}