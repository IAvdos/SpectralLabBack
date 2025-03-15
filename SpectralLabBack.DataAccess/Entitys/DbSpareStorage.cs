using System.ComponentModel.DataAnnotations;

public class DbSpareStorage
{
	public Guid Id { get; set; }
	public string Laboratory { get; set; } = String.Empty;

	public int AvailableCount { get; set; }

	[Required]
	public Guid SpareId { get; set; }
	public DbSpare Spare { get; set; } = null!;
}