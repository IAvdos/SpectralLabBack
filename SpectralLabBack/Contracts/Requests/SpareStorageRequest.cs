public class SpareStorageRequest
{
	public SpareStorageRequest(Guid id, string laboratory, int availableCount, Guid spareId)
	{
		Id = id;
		Laboratory = laboratory;
		AvailableCount = availableCount;
		SpareId = spareId;
	}

	public Guid Id { get; }
	public string Laboratory { get; } = String.Empty;

	public int AvailableCount { get; }

	public Guid SpareId { get; }
}