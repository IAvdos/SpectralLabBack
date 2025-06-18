public class AvailableSpare
{
	public AvailableSpare(
		Guid id, 
		string laboratory, 
		int availableCount, 
		Guid spareId,
		string name,
		string equipment,
		string catalogName,
		int ozmId)
	{
		Id = id;
		Laboratory = laboratory;
		AvailableCount = availableCount;
		SpareId = spareId;
		Name = name;
		Equipment = equipment;
		CatalogName = catalogName;
		OzmId = ozmId;
	}

	public Guid Id { get; }
	public string Laboratory { get; } = String.Empty;
	public int AvailableCount { get; }
	public Guid SpareId { get; }
	public string Name { get; } = String.Empty;
	public string Equipment { get; } = String.Empty;
	public string CatalogName { get; } = String.Empty;
	public int OzmId { get; }

}