public class DbSpareStorage
{
	public Guid Id { get; set; }
	public string Laboratory { get; set; } = String.Empty;

	public int AvailableCount { get; set; }

	public Guid SpareId { get; set; }
	public DbSpare Spare { get; set; } = null!;
}