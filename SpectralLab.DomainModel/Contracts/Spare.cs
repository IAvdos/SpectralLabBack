public class Spare
{
	public Spare(Guid id, string name, string equipment, string catalogName, int ozmId)
	{
		Id = id;
		Name = name;
		Equipment = equipment;
		CatalogName = catalogName;
		OzmId = ozmId;
	}

	public Guid Id { get; }
	public string Name { get; } = String.Empty;
	public string Equipment { get; } = String.Empty;
	public string CatalogName { get; } = String.Empty;
	public int OzmId { get; }
}