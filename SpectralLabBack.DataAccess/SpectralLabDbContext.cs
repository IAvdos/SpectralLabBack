using Microsoft.EntityFrameworkCore;

namespace SpectralLabBack.DataAccess
{
	public class SpectralLabDbContext : DbContext
	{
		public SpectralLabDbContext(DbContextOptions<SpectralLabDbContext> options)
			: base(options)
		{
			//Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		public DbSet<DbProposal> Proposals { get; set; } = null!;
		public DbSet<DbSpare> Spares { get; set; } = null!;
		public DbSet<DbProposalSpareCount> ProposalsSpareCount { get; set; } = null!;
		public DbSet<DbSpareStorage> SparesStorage { get; set; } = null!;
	}
}
