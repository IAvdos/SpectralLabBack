public class ProposalSpareCountRequest
{
	public ProposalSpareCountRequest(Guid id, int count, int receivedCount, Guid spareId)
	{
		Id = id;
		Count = count;
		ReceivedCount = receivedCount;
		SpareId = spareId;
	}

	public Guid Id { get; }
	public int Count { get; set; }
	public int ReceivedCount { get; }
	public Guid SpareId { get; }
}