public class SparesRepository
{
	private readonly DbSparesRepository _dbSparesRepository;

	public SparesRepository(DbSparesRepository dbSparesRepository)
	{
		_dbSparesRepository = dbSparesRepository;
	}

	public async Task<Guid> Create(NewSpareRequest newSpare)
	{
		var spare = new Spare(new Guid(), newSpare.Name, newSpare.Equipment, newSpare.CatalogName, newSpare.OzmId);

		var result = await _dbSparesRepository.Create(spare);

		return result;
	}

	public async Task<List<Spare>> GetAll()
	{
		return await _dbSparesRepository.GetAll();
	}
}