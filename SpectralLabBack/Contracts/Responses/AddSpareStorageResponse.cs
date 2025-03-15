public class AddSpareStorageResponse
{
	public AddSpareStorageResponse(Guid spareId, string laboratory)
	{
		SpareId = spareId;
		Laboratory = laboratory;
	}

	public Guid SpareId {  get; }
	public string Laboratory {  get; } = string.Empty;
}