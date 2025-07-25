﻿public class SparesStorageRepository
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


	public async Task<List<AvailableSpare>> GetWithSpare()
	{
		return await _dbSparesStorageRepository.GetWithSpare();
	}


	public async Task<Guid> RemoveSpareStorageAsync(Guid id)
	{
		return await _dbSparesStorageRepository.RemoveAsync(id);
	}


	public async Task<List<SpareStorage>> UpdateSpares(List<SpareStorage> storages)
	{
		return await _dbSparesStorageRepository.UpadateAsync(storages);
	}
}