using Microsoft.EntityFrameworkCore;
using SpectralLabBack.DataAccess;

public class DbSpareStorageRepositiry
{
	private readonly SpectralLabDbContext _dbContext;

	public DbSpareStorageRepositiry(SpectralLabDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<List<SpareStorage>> GetAsync()
	{
		var spares = await _dbContext.SparesStorage.ToListAsync();

		return spares.Select( s => new SpareStorage(s.Id, s.Laboratory, s.AvailableCount, s.SpareId)).ToList();
	}

	public async Task<List<SpareStorage>> AddAsync(List<SpareStorage> sparesStorage)
	{
		var newSpares = sparesStorage.Where( s => _dbContext.SparesStorage.FirstOrDefault( a => a.SpareId != s.SpareId && a.Laboratory != s.Laboratory) == null ).ToList();
		var conteinsSpares = sparesStorage.Except(newSpares).ToList();

		var newDbSpares = ToDbSpareStorage(newSpares);

		await _dbContext.SparesStorage.AddRangeAsync(newDbSpares);
		_dbContext.SaveChanges();

		return conteinsSpares;
	}

	public async Task UpdateBySummAsync(List<SpareStorage> sparesStorage)
	{
		var newSpares = sparesStorage.Where(s => _dbContext.SparesStorage.FirstOrDefault(a => a.SpareId != s.SpareId && a.Laboratory != s.Laboratory) == null).ToList();
		var conteinsSpares = sparesStorage.Except(newSpares).ToList();

		var result = await AddAsync(newSpares);

		//TODO: do it how to add summ in spares already contein in storage

	}

	public async Task UpadateAsync(List<SpareStorage> sparesStorage)
	{
		// TODO: it method for update in sparestorage without summ
	}

	public async Task<Guid> RemoveAsync(Guid id)
	{
		var removedSpare = _dbContext.SparesStorage.First( a => a.Id == id);

		_dbContext.SparesStorage.Remove(removedSpare);

		await _dbContext.SaveChangesAsync();

		return removedSpare.Id;
	}

	private Tuple<List<SpareStorage>, List<SpareStorage>> DefindConteinsAndNewSpares(List<SpareStorage> sparesStorage)
	{
		var newSpares = sparesStorage.Where(s => _dbContext.SparesStorage.FirstOrDefault(a => a.SpareId != s.SpareId && a.Laboratory != s.Laboratory) == null).ToList();
		var conteinsSpares = sparesStorage.Except(newSpares).ToList();

		return Tuple.Create(newSpares, conteinsSpares);
	}

	private List<DbSpareStorage> ToDbSpareStorage(List<SpareStorage> sparesStorage)
	{
		return sparesStorage.Select( s => 
			new DbSpareStorage
			{
				Id = s.Id,
				SpareId = s.SpareId,
				AvailableCount = s.AvailableCount,
				Laboratory = s.Laboratory
			}).ToList();
	}
}