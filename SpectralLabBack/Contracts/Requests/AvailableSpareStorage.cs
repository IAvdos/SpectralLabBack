public class AvailableSpareStorage
{
	public AvailableSpareStorage(
		string labNameFilter,
		string searchValue,
		bool joinedSpares,
		int pageSize,
		int pageNumber)
	{
		LabNameFilter = labNameFilter;
		SearchValue = searchValue;
		JoinedSpares = joinedSpares;
		PageSize = pageSize;
		PageNumber = pageNumber;
	}

	public string LabNameFilter { get; set; }
	public string SearchValue {  get; set; }
	public bool JoinedSpares { get; set; }
	public int PageSize { get; set; }
	public int PageNumber { get; set; }
}