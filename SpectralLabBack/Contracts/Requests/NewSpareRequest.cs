public class NewSpareRequest
{
	public NewSpareRequest( string name, string equipment, string catalogName, int ozmId)
	{
		Name = name;
		Equipment = equipment;
		CatalogName = catalogName;
		OzmId = ozmId;
	}

	public string Name { get; } = String.Empty;
	public string Equipment { get; } = String.Empty;
	public string CatalogName { get; } = String.Empty;
	public int OzmId { get; }
}
