using Microsoft.EntityFrameworkCore;
using SpectralLabBack.DataAccess;

public class DbSparesRepository
{
	private SpectralLabDbContext _context;

	public DbSparesRepository(SpectralLabDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> Create(Spare spare)
	{
		var newSpare = new DbSpare
		{
			Id = spare.Id,
			Name = spare.Name,
			CatalogName = spare.CatalogName,
			Equipment = spare.Equipment,
			OzmId = spare.OzmId,
		};
		//TODO check the spare will addied then return id or exeption
		await _context.Spares.AddAsync(newSpare);
		await _context.SaveChangesAsync();

		return newSpare.Id;
	}

	public async Task<List<Spare>> GetAll()
	{
		var dbSpares = await _context.Spares.ToListAsync();

		return dbSpares.Select( s => 
			new Spare(
				s.Id,
				s.Name,
				s.Equipment,
				s.CatalogName,
				s.OzmId)
			).ToList();
	}
}