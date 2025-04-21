public class SpareStorageResponse
{
	public SpareStorageResponse(Guid id, string laboratory, int availableCount, Guid spareId )
	{
		Id = id;
		Laboratory = laboratory;
		AvailableCount = availableCount;
		SpareId = spareId;
	}

	public Guid Id { get; }
	public string Laboratory {  get; } = string.Empty;
	public int AvailableCount { get; }
	public Guid SpareId {  get; }
}