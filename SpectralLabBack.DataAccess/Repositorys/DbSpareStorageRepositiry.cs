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

		return spares.Select( s => new SpareStorage(
			s.Id,
			s.Laboratory,
			s.AvailableCount,
			s.SpareId)).ToList();
	}


	public async Task<List<AvailableSpare>> GetWithSpare()
	{
		var spares = await _dbContext.SparesStorage.Include(s => s.Spare).ToListAsync();

		return spares.Select(s => new AvailableSpare(
			s.Id,
			s.Laboratory,
			s.AvailableCount,
			s.SpareId,
			s.Spare.Name,
			s.Spare.Equipment,
			s.Spare.CatalogName,
			s.Spare.OzmId)).ToList();
	}

	public async Task<List<SpareStorage>> AddAsync(List<SpareStorage> sparesStorage)
	{
		var newSpares = sparesStorage.Where( s => _dbContext.SparesStorage.FirstOrDefault( a => a.SpareId == s.SpareId && a.Laboratory == s.Laboratory) == null ).ToList();
		var conteinsSpares = sparesStorage.Except(newSpares).ToList();

		var newDbSpares = ToDbSpareStorage(newSpares);

		await _dbContext.SparesStorage.AddRangeAsync(newDbSpares);
		_dbContext.SaveChanges();

		return newSpares;
	}


	public async Task<List<SpareStorage>> UpadateAsync(List<SpareStorage> sparesStorage)
	{
		List<SpareStorage> updatedSpares = new();
		var newSpares = sparesStorage.Where(p => p.Id == Guid.Empty).ToList();
		var conteinsSpares = sparesStorage.Except(newSpares);
		//await AddAsync(newSpares);
		
		var sparesInDb = _dbContext.SparesStorage.ToList();

		foreach (var spare in conteinsSpares)
		{
			var currentSpare = sparesInDb.FirstOrDefault(p => p.SpareId == spare.SpareId);

			if (currentSpare != null)
			{
				currentSpare.AvailableCount = spare.AvailableCount;
				updatedSpares.Add(spare);
			}
		}

		await _dbContext.SaveChangesAsync();

		return updatedSpares;
	}

	public async Task<Guid> RemoveAsync(Guid id)
	{
		var removedSpare = _dbContext.SparesStorage.FirstOrDefault( a => a.Id == id);

		_dbContext.SparesStorage.Remove(removedSpare);

		await _dbContext.SaveChangesAsync();

		return removedSpare.Id;
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