using System.Runtime.CompilerServices;

public class ProposalManager
{
	private readonly DbProposalsRepository _dbProposalRepository;
	private readonly DbProposalSparesRepository _dbProposalSparesRepository;
	private readonly DbSpareStorageRepositiry _dbSparesStorageRepository;
	/*
	private readonly ProposalsRepository _proposalRepository;
	private readonly SparesStorageRepository _storageRepository;

	public ProposalManager(ProposalsRepository proposalsRepository, SparesStorageRepository storageRepository)
	{
		_proposalRepository = proposalsRepository;
		_storageRepository = storageRepository;
	}
	*/
	public ProposalManager(DbProposalsRepository dbproposalRepository, DbProposalSparesRepository dbProposalSparesRepository, DbSpareStorageRepositiry dbSpareStorageRepositiry)
	{
		_dbProposalRepository = dbproposalRepository;
		_dbProposalSparesRepository = dbProposalSparesRepository;
		_dbSparesStorageRepository = dbSpareStorageRepositiry;
	}


	public async Task<Guid> UpdateAsync(Proposal proposal)
	{
		var dbProposal = await _dbProposalRepository.FindAsync(proposal.Id);

		if (dbProposal == null)
		{
			return Guid.Empty;
		}

		if (dbProposal.IsFinal)
		{
			await UpateSparesStorageAsync(proposal.SparesCount, proposal.Laboratory);
		}

		return await UpdateProposalAsync(proposal);
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

	private async Task UpateSparesStorageAsync(List<ProposalSpareCount> sparesCount, string laboratory)
	{
		var sparesStorage = await _dbSparesStorageRepository.GetAsync();
		var newSpares = sparesCount.Where( s => sparesStorage.FirstOrDefault( x => x.SpareId == s.SpareId && x.Laboratory == laboratory) == null).ToList();
		var conteinsSpares = sparesCount.Except(newSpares).ToList();

		var newDbSpares = newSpares.Select(s =>
			new SpareStorage
				(
					Guid.NewGuid(),
					laboratory,
					s.ReceivedCount,
					s.SpareId
				)).ToList();

		await _dbSparesStorageRepository.AddAsync(newDbSpares);


		var dbConteinsSpares = sparesStorage.Where( s => conteinsSpares.FirstOrDefault(x => x.SpareId == s.SpareId && s.Laboratory == laboratory) != null).ToList();

		var increasedCountSpares = await DefineIncreaseSpareCountInProposal(conteinsSpares);

		var dbSparesToUpdate = IncreaseAvailableCount(dbConteinsSpares, increasedCountSpares);
		//TODO: returned tupes?
		await _dbSparesStorageRepository.UpadateAsync(dbSparesToUpdate);
	}


	private async Task<List<ProposalSpareCount>> DefineIncreaseSpareCountInProposal(List<ProposalSpareCount> sparesCount)
	{
		var dbSpares = await _dbProposalSparesRepository.GetAsync();
		var sameSpares = dbSpares.Where( s => sparesCount.FirstOrDefault( x => x.Id == s.Id) != null).ToList();

		var sameSperesWithCountDifference = new List<ProposalSpareCount>();

		foreach ( var s in sameSpares)
		{
			foreach ( var p in sparesCount)
			{
				if( s.Id == p.Id)
				{
					var spare = new ProposalSpareCount
						(
							s.Id,
							s.Count,
							p.ReceivedCount - s.ReceivedCount,
							s.SpareId
						);

					sameSperesWithCountDifference.Add( spare );
				}
			}
		}

		return sameSperesWithCountDifference;
	}

	private List<SpareStorage> IncreaseAvailableCount(List<SpareStorage> spareStorages, List<ProposalSpareCount> increaseSparesCount)
	{
		var result = new List<SpareStorage>();

		foreach ( var s in spareStorages)
		{
			foreach( var p in increaseSparesCount)
			{
				if( s.SpareId == p.SpareId)
				{
					var spare = new SpareStorage(s.Id, s.Laboratory, s.AvailableCount + p.ReceivedCount, s.SpareId );

					result.Add(spare);
				}
			}
		}

		return result;
	}

}