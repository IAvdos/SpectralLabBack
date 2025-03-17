using Microsoft.Extensions.Logging;

public class ProposalsRepository
{
	private readonly DbProposalsRepository _dbProposalRepository;
	private readonly DbProposalSparesRepository _dbProposalSparesRepository;
	private readonly DbSpareStorageRepositiry _dbSparesStorageRepository;

	public ProposalsRepository(DbProposalsRepository dbproposalRepository, DbProposalSparesRepository dbProposalSparesRepository, DbSpareStorageRepositiry dbSpareStorageRepositiry)
	{
		_dbProposalRepository = dbproposalRepository;
		_dbProposalSparesRepository = dbProposalSparesRepository;
		_dbSparesStorageRepository = dbSpareStorageRepositiry;
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

	/*
	public async Task<Guid> UpdateAsync(Proposal proposal)
	{
		var dbProposal = await _dbProposalRepository.FindAsync( proposal.Id );

		if ( dbProposal == null )
		{
			return Guid.Empty;
		}

		if ( dbProposal.IsFinal )
		{
			UpateSparesStorageAsync(proposal.SparesCount);
		}
		
		return await UpdateProposalAsync( proposal );
	}



	private async Task UpateSparesStorageAsync(List<ProposalSpareCount> sparesCount, string laboratory)
	{
		var sparesStorage = sparesCount.Select( s => 
			new SpareStorage
				(
					Guid.NewGuid(),
					laboratory,
					s.ReceivedCount,
					s.SpareId
				)).ToList();

		_dbSparesStorageRepository.AddAsync(sparesStorage);
	}
	*/
}