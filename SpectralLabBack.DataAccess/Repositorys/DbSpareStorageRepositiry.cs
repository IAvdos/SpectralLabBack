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