public class SparesStorageRepository
{
	private readonly DbSpareStorageRepositiry _dbSparesStorageRepository;

	public SparesStorageRepository(DbSpareStorageRepositiry dbSpareStorageRepositiry)
	{
		_dbSparesStorageRepository = dbSpareStorageRepositiry;
	}

	public async Task<List<SpareStorage>> CreateSparesStorageAsync(List<SpareStorage> spareStorages)
	{
		return await _dbSparesStorageRepository.AddAsync(spareStorages);
	}

	public async Task<List<SpareStorage>> GetAllAsync()
	{
		return await _dbSparesStorageRepository.GetAsync();
	}

	public async Task<Guid> RemoveSpareStorageAsync(Guid id)
	{
		return await _dbSparesStorageRepository.RemoveAsync(id);
	}
}